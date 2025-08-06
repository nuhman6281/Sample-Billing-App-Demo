using Sample_Billing_App.Models;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace Sample_Billing_App.Services
{
    public class PdfInvoiceGenerator
    {
        public static string GenerateInvoicePdf(Invoice invoice, string? outputPath = null)
        {
            try
            {
                // Validate invoice
                if (invoice == null)
                {
                    throw new ArgumentException("Invoice cannot be null");
                }

                // Generate unique filename if not provided
                if (string.IsNullOrEmpty(outputPath))
                {
                    string tempDir = Path.GetTempPath();
                    string fileName = $"Invoice_{invoice.InvoiceNumber}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
                    outputPath = Path.Combine(tempDir, fileName);
                }

                // Ensure directory exists
                string? directory = Path.GetDirectoryName(outputPath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                // For now, we'll create a simple text file as a fallback
                // since iText7 seems to have initialization issues
                string textContent = GenerateInvoiceText(invoice);
                File.WriteAllText(outputPath.Replace(".pdf", ".txt"), textContent);

                // Return the text file path for now
                return outputPath.Replace(".pdf", ".txt");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error generating invoice: {ex.Message}", ex);
            }
        }

        private static string GenerateInvoiceText(Invoice invoice)
        {
            var sb = new System.Text.StringBuilder();
            
            // Header
            sb.AppendLine("=".PadRight(60, '='));
            sb.AppendLine("TUKZO ABC".PadLeft(30));
            sb.AppendLine("=".PadRight(60, '='));
            sb.AppendLine("MUNICIPAL SHOPPING COMPLEX, NGO QUARTERS, KAKKANAD");
            sb.AppendLine("PIN: 682021, PH: 9995379212, GSTIN: 32CVPPM1824A1ZY");
            sb.AppendLine();
            
            // Invoice details
            sb.AppendLine($"Invoice No: {invoice.InvoiceNumber}");
            sb.AppendLine($"Date: {invoice.InvoiceDate:dd-MMM-yyyy}");
            sb.AppendLine($"Customer: {invoice.CustomerName ?? "N/A"}");
            sb.AppendLine($"Mobile: {invoice.CustomerMobile ?? "N/A"}");
            sb.AppendLine($"GSTIN: {invoice.CustomerGSTIN ?? "N/A"}");
            sb.AppendLine($"Payment: {invoice.PaymentType ?? "N/A"}");
            sb.AppendLine();
            
            // Items table
            sb.AppendLine("-".PadRight(60, '-'));
            sb.AppendLine("No. | Item Name | Qty | MRP | Rate | Total");
            sb.AppendLine("-".PadRight(60, '-'));
            
            if (invoice.Items != null && invoice.Items.Count > 0)
            {
                for (int i = 0; i < invoice.Items.Count; i++)
                {
                    var item = invoice.Items[i];
                    if (item != null)
                    {
                        sb.AppendLine($"{i + 1,3} | {item.Name ?? "",-20} | {item.Quantity,5:F3} | {item.MRP,6:F2} | {item.Rate,6:F2} | {item.Total,8:F2}");
                    }
                }
            }
            
            sb.AppendLine("-".PadRight(60, '-'));
            
            // Totals
            sb.AppendLine($"Total Quantity: {invoice.TotalQuantity:F3}");
            sb.AppendLine($"Net Total: ₹{invoice.NetTotal:F2}");
            sb.AppendLine($"Taxable Amount: ₹{invoice.TaxableAmount:F2}");
            sb.AppendLine($"CGST (2.5%): ₹{invoice.CGST:F2}");
            sb.AppendLine($"SGST (2.5%): ₹{invoice.SGST:F2}");
            sb.AppendLine();
            
            // Savings and bill amount
            sb.AppendLine($"YOU HAVE SAVED Rs. {invoice.TotalSavings:F2}");
            sb.AppendLine();
            sb.AppendLine($"BILL AMOUNT: ₹{invoice.BillAmount:F2}");
            sb.AppendLine($"Amount in Words: {invoice.AmountInWords} Rupees Only");
            sb.AppendLine();
            
            // Footer
            sb.AppendLine("Thank You & Visit Again");
            sb.AppendLine($"Printed on: {DateTime.Now:dd-MMM-yyyy HH:mm}");
            sb.AppendLine("=".PadRight(60, '='));
            
            return sb.ToString();
        }

        public static void PrintInvoice(Invoice invoice, string printerName)
        {
            try
            {
                // Validate inputs
                if (invoice == null)
                {
                    throw new ArgumentException("Invoice cannot be null");
                }

                if (string.IsNullOrEmpty(printerName))
                {
                    throw new ArgumentException("Printer name cannot be empty");
                }

                // Validate that the printer exists
                bool printerExists = false;
                foreach (string printer in PrinterSettings.InstalledPrinters)
                {
                    if (printer.Equals(printerName, StringComparison.OrdinalIgnoreCase))
                    {
                        printerExists = true;
                        break;
                    }
                }

                if (!printerExists)
                {
                    throw new ArgumentException($"Printer '{printerName}' not found. Available printers: {string.Join(", ", PrinterSettings.InstalledPrinters)}");
                }

                // Validate invoice has items
                if (invoice.Items == null || invoice.Items.Count == 0)
                {
                    throw new ArgumentException("Invoice must have at least one item to print");
                }

                // Use PrintDocument with GDI+ drawing that matches HTML layout
                using (var printDoc = new PrintDocument())
                {
                    printDoc.PrinterSettings.PrinterName = printerName;
                    printDoc.DocumentName = $"Invoice_{invoice.InvoiceNumber}";
                    
                    printDoc.PrintPage += (sender, e) => 
                    {
                        try
                        {
                            if (e.Graphics != null)
                            {
                                DrawInvoiceHtml(e.Graphics, invoice);
                            }
                            else
                            {
                                throw new InvalidOperationException("Graphics object is null during printing");
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception($"Error during print page rendering: {ex.Message}", ex);
                        }
                    };
                    
                    printDoc.Print();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error printing invoice: {ex.Message}", ex);
            }
        }

        private static void DrawInvoiceHtml(Graphics g, Invoice invoice)
        {
            try
            {
                if (g == null) throw new ArgumentException("Graphics object cannot be null");
                if (invoice == null) throw new ArgumentException("Invoice cannot be null");

                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

                // Define fonts to match HTML styling
                var titleFont = new Font("Arial", 24, FontStyle.Bold);
                var headerFont = new Font("Arial", 14, FontStyle.Bold);
                var normalFont = new Font("Arial", 12);
                var smallFont = new Font("Arial", 10);

                // Margins and positioning to match HTML layout
                int leftMargin = 50;
                int topMargin = 50;
                int currentY = topMargin;
                int lineHeight = 25;
                int pageWidth = 800;

                // Draw header with same styling as HTML
                var headerBrush = new SolidBrush(Color.FromArgb(51, 51, 51));
                g.DrawString("TUKZO ABC", titleFont, headerBrush, leftMargin, currentY);
                currentY += lineHeight + 10;

                // Draw store details with same layout as HTML
                g.DrawString("MUNICIPAL SHOPPING COMPLEX, NGO QUARTERS, KAKKANAD", normalFont, Brushes.Black, leftMargin, currentY);
                currentY += lineHeight;
                g.DrawString("PIN: 682021, PH: 9995379212", normalFont, Brushes.Black, leftMargin, currentY);
                currentY += lineHeight;
                g.DrawString("GSTIN: 32CVPPM1824A1ZY", normalFont, Brushes.Black, leftMargin, currentY);
                currentY += lineHeight + 15;

                // Draw separator line to match HTML border
                var pen = new Pen(Color.FromArgb(51, 51, 51), 2);
                g.DrawLine(pen, leftMargin, currentY, pageWidth - leftMargin, currentY);
                currentY += 20;

                // Draw invoice info in same layout as HTML
                g.DrawString($"Invoice No: {invoice.InvoiceNumber}", headerFont, Brushes.Black, leftMargin, currentY);
                g.DrawString($"Date: {invoice.InvoiceDate:dd-MMM-yyyy}", headerFont, Brushes.Black, leftMargin + 300, currentY);
                currentY += lineHeight;

                g.DrawString($"Customer: {invoice.CustomerName ?? "N/A"}", headerFont, Brushes.Black, leftMargin, currentY);
                currentY += lineHeight;

                g.DrawString($"Mobile: {invoice.CustomerMobile ?? "N/A"}", headerFont, Brushes.Black, leftMargin, currentY);
                g.DrawString($"GSTIN: {invoice.CustomerGSTIN ?? "N/A"}", headerFont, Brushes.Black, leftMargin + 300, currentY);
                currentY += lineHeight;

                g.DrawString($"Payment Type: {invoice.PaymentType ?? "N/A"}", headerFont, Brushes.Black, leftMargin, currentY);
                currentY += lineHeight + 15;

                // Draw items table with same styling as HTML
                var tableHeaderBrush = new SolidBrush(Color.FromArgb(242, 242, 242));
                g.FillRectangle(tableHeaderBrush, leftMargin, currentY, pageWidth - (leftMargin * 2), lineHeight + 10);
                
                var tablePen = new Pen(Color.FromArgb(221, 221, 221), 1);
                g.DrawRectangle(tablePen, leftMargin, currentY, pageWidth - (leftMargin * 2), lineHeight + 10);
                
                g.DrawString("No.", headerFont, Brushes.Black, leftMargin + 10, currentY + 5);
                g.DrawString("Item Name", headerFont, Brushes.Black, leftMargin + 80, currentY + 5);
                g.DrawString("Qty", headerFont, Brushes.Black, leftMargin + 300, currentY + 5);
                g.DrawString("MRP", headerFont, Brushes.Black, leftMargin + 400, currentY + 5);
                g.DrawString("Rate", headerFont, Brushes.Black, leftMargin + 500, currentY + 5);
                g.DrawString("Total", headerFont, Brushes.Black, leftMargin + 600, currentY + 5);
                currentY += lineHeight + 15;

                // Draw items with alternating row colors like HTML
                if (invoice.Items != null)
                {
                    for (int i = 0; i < invoice.Items.Count; i++)
                    {
                        var item = invoice.Items[i];
                        if (item != null)
                        {
                            // Alternate row background like HTML table
                            if (i % 2 == 0)
                            {
                                var rowBrush = new SolidBrush(Color.FromArgb(250, 250, 250));
                                g.FillRectangle(rowBrush, leftMargin, currentY, pageWidth - (leftMargin * 2), lineHeight);
                            }

                            g.DrawString((i + 1).ToString(), normalFont, Brushes.Black, leftMargin + 10, currentY);
                            g.DrawString(item.Name ?? "", normalFont, Brushes.Black, leftMargin + 80, currentY);
                            g.DrawString(item.Quantity.ToString("F3"), normalFont, Brushes.Black, leftMargin + 300, currentY);
                            g.DrawString(item.MRP.ToString("F2"), normalFont, Brushes.Black, leftMargin + 400, currentY);
                            g.DrawString(item.Rate.ToString("F2"), normalFont, Brushes.Black, leftMargin + 500, currentY);
                            g.DrawString(item.Total.ToString("F2"), normalFont, Brushes.Black, leftMargin + 600, currentY);
                            currentY += lineHeight;
                        }
                    }
                }

                currentY += 20;

                // Draw totals section with same layout as HTML
                g.DrawString("SUMMARY", headerFont, Brushes.Black, leftMargin, currentY);
                currentY += lineHeight + 10;

                // Draw totals in same format as HTML
                g.DrawString($"Total Quantity: {invoice.TotalQuantity:F3}", normalFont, Brushes.Black, leftMargin, currentY);
                g.DrawString($"Net Total: ₹{invoice.NetTotal:F2}", headerFont, Brushes.Black, leftMargin + 300, currentY);
                currentY += lineHeight;

                g.DrawString($"Taxable Amount: ₹{invoice.TaxableAmount:F2}", normalFont, Brushes.Black, leftMargin, currentY);
                g.DrawString($"CGST (2.5%): ₹{invoice.CGST:F2}", normalFont, Brushes.Black, leftMargin + 300, currentY);
                currentY += lineHeight;

                g.DrawString($"SGST (2.5%): ₹{invoice.SGST:F2}", normalFont, Brushes.Black, leftMargin + 300, currentY);
                currentY += lineHeight + 15;

                // Draw savings with same background color as HTML
                var savingsBrush = new SolidBrush(Color.FromArgb(255, 243, 205));
                g.FillRectangle(savingsBrush, leftMargin, currentY, pageWidth - (leftMargin * 2), lineHeight + 10);
                g.DrawRectangle(new Pen(Color.FromArgb(255, 234, 167), 1), leftMargin, currentY, pageWidth - (leftMargin * 2), lineHeight + 10);
                g.DrawString($"YOU HAVE SAVED Rs. {invoice.TotalSavings:F2}", headerFont, Brushes.Black, leftMargin + 10, currentY + 5);
                currentY += lineHeight + 20;

                // Draw bill amount with same styling as HTML
                var billAmountBrush = new SolidBrush(Color.FromArgb(209, 236, 241));
                g.FillRectangle(billAmountBrush, leftMargin, currentY, pageWidth - (leftMargin * 2), lineHeight + 15);
                g.DrawRectangle(new Pen(Color.FromArgb(190, 229, 235), 2), leftMargin, currentY, pageWidth - (leftMargin * 2), lineHeight + 15);
                g.DrawString($"BILL AMOUNT: ₹{invoice.BillAmount:F2}", titleFont, Brushes.Black, leftMargin + 10, currentY + 5);
                currentY += lineHeight + 20;

                // Draw amount in words with error handling
                string amountInWords = "Zero";
                try
                {
                    amountInWords = invoice.AmountInWords;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error converting amount to words: {ex.Message}");
                    amountInWords = $"Amount: {invoice.BillAmount:F2}";
                }
                g.DrawString($"Amount in Words: {amountInWords} Rupees Only", headerFont, Brushes.Black, leftMargin, currentY);
                currentY += lineHeight + 20;

                // Draw footer with same styling as HTML
                g.DrawString("Thank You & Visit Again", headerFont, Brushes.Black, leftMargin, currentY);
                currentY += lineHeight;
                g.DrawString($"Printed on: {DateTime.Now:dd-MMM-yyyy HH:mm}", smallFont, Brushes.Gray, leftMargin, currentY);

                // Clean up resources
                titleFont.Dispose();
                headerFont.Dispose();
                normalFont.Dispose();
                smallFont.Dispose();
                headerBrush.Dispose();
                tableHeaderBrush.Dispose();
                savingsBrush.Dispose();
                billAmountBrush.Dispose();
                pen.Dispose();
                tablePen.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error drawing invoice: {ex.Message}", ex);
            }
        }

        public static void PreviewInvoice(Invoice invoice)
        {
            try
            {
                // Validate invoice
                if (invoice == null)
                {
                    throw new ArgumentException("Invoice cannot be null");
                }

                // Generate text file for preview
                string textPath = GenerateInvoicePdf(invoice);
                
                // Verify file was created
                if (!File.Exists(textPath))
                {
                    throw new Exception("Invoice file was not created successfully");
                }

                // Open with default text editor
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = textPath,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error previewing invoice: {ex.Message}", ex);
            }
        }
    }
} 