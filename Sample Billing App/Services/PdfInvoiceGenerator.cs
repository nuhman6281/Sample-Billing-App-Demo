using Sample_Billing_App.Models;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace Sample_Billing_App.Services
{
    public class PdfInvoiceGenerator
    {
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

                // Use PrintDocument with flexible sizing
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
                                // Use the full printable area available
                                DrawInvoiceHtml(e.Graphics, invoice, e.PageBounds.Width, e.PageBounds.Height);
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

        public class InvoiceConfig
        {
            // Flexible configuration - percentages and ratios instead of fixed sizes
            public const int MIN_PADDING = 10;              // Minimum padding
            public const double PADDING_RATIO = 0.02;       // 2% of width for padding
            public const double LINE_HEIGHT_RATIO = 0.03;   // 3% of width for line height
            public const double SECTION_SPACING_RATIO = 0.025; // 2.5% of width for section spacing
        }

        public static string GenerateInvoiceHtml(Invoice invoice)
        {
            var sb = new System.Text.StringBuilder();

            // Start HTML document - fully responsive
            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html>");
            sb.AppendLine("<head>");
            sb.AppendLine("<meta charset='utf-8'>");
            sb.AppendLine("<title>Invoice Preview</title>");
            sb.AppendLine("<style>");
            sb.AppendLine("@media print {");
            sb.AppendLine("  body { margin: 0; padding: 0; }");
            sb.AppendLine("  .invoice-container { max-width: none; margin: 0; border: none; }");
            sb.AppendLine("  .no-print { display: none; }");
            sb.AppendLine("}");
            sb.AppendLine("body { font-family: Arial, sans-serif; margin: 0; padding: 0; background-color: #f5f5f5; }");
            sb.AppendLine(".invoice-container { width: 100%; height: 100vh; margin: 0 auto; background-color: white; padding: 2%; box-sizing: border-box; }");
            sb.AppendLine(".header { text-align: center; border-bottom: 2px solid #333; padding-bottom: 2%; margin-bottom: 3%; }");
            sb.AppendLine(".store-name { font-size: 4vw; font-weight: bold; color: #333; line-height: 1.2; }");
            sb.AppendLine(".store-details { font-size: 2vw; color: #666; margin-top: 1%; line-height: 1.3; }");
            sb.AppendLine(".invoice-info { margin-bottom: 3%; }");
            sb.AppendLine(".info-row { display: flex; justify-content: space-between; margin-bottom: 1%; font-size: 2.5vw; line-height: 1.4; }");
            sb.AppendLine(".info-label { font-weight: bold; }");
            sb.AppendLine("table { width: 100%; border-collapse: collapse; margin: 3% 0; font-size: 2vw; }");
            sb.AppendLine("th, td { border: 1px solid #ddd; padding: 1%; text-align: left; }");
            sb.AppendLine("th { background-color: #f2f2f2; font-weight: bold; }");
            sb.AppendLine(".totals { margin-top: 3%; }");
            sb.AppendLine(".total-row { display: flex; justify-content: space-between; margin-bottom: 1%; font-weight: bold; font-size: 2.5vw; line-height: 1.4; }");
            sb.AppendLine(".savings { background-color: #fff3cd; padding: 2%; text-align: center; margin: 2% 0; border: 1px solid #ffeaa7; font-size: 2.5vw; }");
            sb.AppendLine(".bill-amount { background-color: #d1ecf1; padding: 2%; text-align: center; font-size: 3vw; font-weight: bold; margin: 3% 0; border: 2px solid #bee5eb; }");
            sb.AppendLine(".amount-words { text-align: center; margin: 2% 0; font-size: 2vw; line-height: 1.3; }");
            sb.AppendLine(".footer { text-align: center; margin-top: 3%; padding-top: 2%; border-top: 1px solid #ddd; color: #666; font-size: 2vw; }");

            // Responsive breakpoints
            sb.AppendLine("@media (max-width: 600px) {");
            sb.AppendLine("  .store-name { font-size: 24px; }");
            sb.AppendLine("  .store-details { font-size: 12px; }");
            sb.AppendLine("  .info-row { font-size: 14px; }");
            sb.AppendLine("  table { font-size: 12px; }");
            sb.AppendLine("  .total-row { font-size: 14px; }");
            sb.AppendLine("  .savings { font-size: 14px; }");
            sb.AppendLine("  .bill-amount { font-size: 18px; }");
            sb.AppendLine("  .amount-words { font-size: 12px; }");
            sb.AppendLine("  .footer { font-size: 12px; }");
            sb.AppendLine("}");

            sb.AppendLine("@media (min-width: 1200px) {");
            sb.AppendLine("  .store-name { font-size: 48px; }");
            sb.AppendLine("  .store-details { font-size: 24px; }");
            sb.AppendLine("  .info-row { font-size: 30px; }");
            sb.AppendLine("  table { font-size: 24px; }");
            sb.AppendLine("  .total-row { font-size: 30px; }");
            sb.AppendLine("  .savings { font-size: 30px; }");
            sb.AppendLine("  .bill-amount { font-size: 36px; }");
            sb.AppendLine("  .amount-words { font-size: 24px; }");
            sb.AppendLine("  .footer { font-size: 24px; }");
            sb.AppendLine("}");

            sb.AppendLine("</style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("<div class='invoice-container'>");

            // Header
            sb.AppendLine("<div class='header'>");
            sb.AppendLine("<div class='store-name'>TUKZO ABC</div>");
            sb.AppendLine("<div class='store-details'>");
            sb.AppendLine("MUNICIPAL SHOPPING COMPLEX<br>NGO QUARTERS, KAKKANAD<br>");
            sb.AppendLine("PIN: 682021, PH: 9995379212<br>");
            sb.AppendLine("GSTIN: 32CVPPM1824A1ZY");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");

            // Invoice details
            sb.AppendLine("<div class='invoice-info'>");
            sb.AppendLine($"<div class='info-row'><span class='info-label'>Invoice No:</span><span>{invoice.InvoiceNumber}</span></div>");
            sb.AppendLine($"<div class='info-row'><span class='info-label'>Date:</span><span>{invoice.InvoiceDate:dd-MMM-yyyy}</span></div>");
            sb.AppendLine($"<div class='info-row'><span class='info-label'>Customer:</span><span>{invoice.CustomerName ?? "N/A"}</span></div>");
            sb.AppendLine($"<div class='info-row'><span class='info-label'>Mobile:</span><span>{invoice.CustomerMobile ?? "N/A"}</span></div>");
            sb.AppendLine($"<div class='info-row'><span class='info-label'>GSTIN:</span><span>{invoice.CustomerGSTIN ?? "N/A"}</span></div>");
            sb.AppendLine($"<div class='info-row'><span class='info-label'>Payment:</span><span>{invoice.PaymentType ?? "N/A"}</span></div>");
            sb.AppendLine("</div>");

            // Items table
            sb.AppendLine("<table>");
            sb.AppendLine("<thead>");
            sb.AppendLine("<tr>");
            sb.AppendLine("<th style='width:8%'>No.</th>");
            sb.AppendLine("<th style='width:40%'>Item</th>");
            sb.AppendLine("<th style='width:13%'>Qty</th>");
            sb.AppendLine("<th style='width:13%'>MRP</th>");
            sb.AppendLine("<th style='width:13%'>Rate</th>");
            sb.AppendLine("<th style='width:13%'>Total</th>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</thead>");
            sb.AppendLine("<tbody>");

            if (invoice.Items != null && invoice.Items.Count > 0)
            {
                for (int i = 0; i < invoice.Items.Count; i++)
                {
                    var item = invoice.Items[i];
                    if (item != null)
                    {
                        sb.AppendLine("<tr>");
                        sb.AppendLine($"<td>{i + 1}</td>");
                        sb.AppendLine($"<td style='word-break: break-word;'>{item.Name ?? ""}</td>");
                        sb.AppendLine($"<td>{item.Quantity:F2}</td>");
                        sb.AppendLine($"<td>{item.MRP:F2}</td>");
                        sb.AppendLine($"<td>{item.Rate:F2}</td>");
                        sb.AppendLine($"<td>{item.Total:F2}</td>");
                        sb.AppendLine("</tr>");
                    }
                }
            }

            sb.AppendLine("</tbody>");
            sb.AppendLine("</table>");

            // Totals
            sb.AppendLine("<div class='totals'>");
            sb.AppendLine($"<div class='total-row'><span>Total Qty:</span><span>{invoice.TotalQuantity:F2}</span></div>");
            sb.AppendLine($"<div class='total-row'><span>Net Total:</span><span>₹{invoice.NetTotal:F2}</span></div>");
            sb.AppendLine($"<div class='total-row'><span>Taxable:</span><span>₹{invoice.TaxableAmount:F2}</span></div>");
            sb.AppendLine($"<div class='total-row'><span>CGST (2.5%):</span><span>₹{invoice.CGST:F2}</span></div>");
            sb.AppendLine($"<div class='total-row'><span>SGST (2.5%):</span><span>₹{invoice.SGST:F2}</span></div>");
            sb.AppendLine("</div>");

            // Savings
            sb.AppendLine($"<div class='savings'>YOU HAVE SAVED Rs. {invoice.TotalSavings:F2}</div>");

            // Bill amount
            sb.AppendLine($"<div class='bill-amount'>BILL AMOUNT: ₹{invoice.BillAmount:F2}</div>");

            // Amount in words
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
            sb.AppendLine($"<div class='amount-words'>Amount in Words: {amountInWords} Rupees Only</div>");

            // Footer
            sb.AppendLine("<div class='footer'>");
            sb.AppendLine("<strong>Thank You & Visit Again</strong><br>");
            sb.AppendLine($"<small>Printed on: {DateTime.Now:dd-MMM-yyyy HH:mm}</small>");
            sb.AppendLine("</div>");

            sb.AppendLine("</div>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            return sb.ToString();
        }

        private static void DrawInvoiceHtml(Graphics g, Invoice invoice, int availableWidth = 0, int availableHeight = 0)
        {
            try
            {
                if (g == null) throw new ArgumentException("Graphics object cannot be null");
                if (invoice == null) throw new ArgumentException("Invoice cannot be null");

                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

                // Use available dimensions or default to reasonable size
                int canvasWidth = availableWidth > 0 ? availableWidth : 800;
                int canvasHeight = availableHeight > 0 ? availableHeight : 1000;

                // REMOVE PADDING - Use full width for print
                int contentPadding = 5; // Minimal padding just for readability
                int sectionSpacing = Math.Max((int)(canvasWidth * 0.01), 5); // 1% spacing or minimum 5px

                // RESPONSIVE FONT CALCULATION - Matching HTML vw units exactly
                float vwUnit = canvasWidth / 100f;

                // Calculate font sizes matching HTML CSS exactly
                float titleFontSize, storeDetailsFontSize, infoRowFontSize, tableFontSize,
                      totalRowFontSize, savingsFontSize, billAmountFontSize, amountWordsFontSize, footerFontSize;

                // Apply responsive breakpoints matching HTML
                if (canvasWidth <= 600)
                {
                    titleFontSize = 24f;
                    storeDetailsFontSize = 12f;
                    infoRowFontSize = 14f;
                    tableFontSize = 12f;
                    totalRowFontSize = 14f;
                    savingsFontSize = 14f;
                    billAmountFontSize = 18f;
                    amountWordsFontSize = 12f;
                    footerFontSize = 12f;
                }
                else if (canvasWidth >= 1200)
                {
                    titleFontSize = 48f;
                    storeDetailsFontSize = 24f;
                    infoRowFontSize = 30f;
                    tableFontSize = 24f;
                    totalRowFontSize = 30f;
                    savingsFontSize = 30f;
                    billAmountFontSize = 36f;
                    amountWordsFontSize = 24f;
                    footerFontSize = 24f;
                }
                else
                {
                    titleFontSize = 4f * vwUnit;
                    storeDetailsFontSize = 2f * vwUnit;
                    infoRowFontSize = 2.5f * vwUnit;
                    tableFontSize = 2f * vwUnit;
                    totalRowFontSize = 2.5f * vwUnit;
                    savingsFontSize = 2.5f * vwUnit;
                    billAmountFontSize = 3f * vwUnit;
                    amountWordsFontSize = 2f * vwUnit;
                    footerFontSize = 2f * vwUnit;
                }

                // Apply minimum font sizes to ensure readability
                titleFontSize = Math.Max(titleFontSize, 10f);
                storeDetailsFontSize = Math.Max(storeDetailsFontSize, 8f);
                infoRowFontSize = Math.Max(infoRowFontSize, 8f);
                tableFontSize = Math.Max(tableFontSize, 7f);
                totalRowFontSize = Math.Max(totalRowFontSize, 8f);
                savingsFontSize = Math.Max(savingsFontSize, 8f);
                billAmountFontSize = Math.Max(billAmountFontSize, 10f);
                amountWordsFontSize = Math.Max(amountWordsFontSize, 7f);
                footerFontSize = Math.Max(footerFontSize, 7f);

                // Create fonts
                var titleFont = new Font("Arial", titleFontSize, FontStyle.Bold);
                var storeDetailsFont = new Font("Arial", storeDetailsFontSize, FontStyle.Regular);
                var infoLabelFont = new Font("Arial", infoRowFontSize, FontStyle.Bold);
                var infoValueFont = new Font("Arial", infoRowFontSize, FontStyle.Regular);
                var tableHeaderFont = new Font("Arial", tableFontSize, FontStyle.Bold);
                var tableCellFont = new Font("Arial", tableFontSize, FontStyle.Regular);
                var totalRowFont = new Font("Arial", totalRowFontSize, FontStyle.Bold);
                var savingsFont = new Font("Arial", savingsFontSize, FontStyle.Regular);
                var billAmountFont = new Font("Arial", billAmountFontSize, FontStyle.Bold);
                var amountWordsFont = new Font("Arial", amountWordsFontSize, FontStyle.Regular);
                var footerMainFont = new Font("Arial", footerFontSize, FontStyle.Bold);
                var footerSmallFont = new Font("Arial", footerFontSize, FontStyle.Regular);

                // Calculate spacing
                int lineHeight = Math.Max((int)(infoRowFontSize * 1.4f), 12);
                int rowHeight = Math.Max((int)(tableFontSize * 2.5f), 16);

                // Define colors
                var backgroundColor = Color.White; // Remove gray background for print
                var headerTextColor = Color.FromArgb(51, 51, 51);
                var tableBorderColor = Color.FromArgb(221, 221, 221);
                var tableHeaderColor = Color.FromArgb(242, 242, 242);
                var savingsColor = Color.FromArgb(255, 243, 205);
                var savingsBorderColor = Color.FromArgb(255, 234, 167);
                var billAmountColor = Color.FromArgb(209, 236, 241);
                var billAmountBorderColor = Color.FromArgb(190, 229, 235);
                var footerColor = Color.FromArgb(102, 102, 102);

                // USE FULL WIDTH - No margins
                int currentY = contentPadding;
                int leftMargin = contentPadding;
                int rightMargin = canvasWidth - contentPadding;
                int contentWidth = canvasWidth - (contentPadding * 2);

                // Fill full background
                g.FillRectangle(new SolidBrush(backgroundColor), 0, 0, canvasWidth, canvasHeight);

                // HEADER SECTION - Full width
                var titleSize = g.MeasureString("TUKZO ABC", titleFont);
                g.DrawString("TUKZO ABC", titleFont, new SolidBrush(headerTextColor),
                    leftMargin + (contentWidth - titleSize.Width) / 2, currentY);
                currentY += (int)(titleSize.Height + sectionSpacing);

                // Store details - full width
                string[] storeDetails = {
            "MUNICIPAL SHOPPING COMPLEX",
            "NGO QUARTERS, KAKKANAD",
            "PIN: 682021, PH: 9995379212",
            "GSTIN: 32CVPPM1824A1ZY"
        };

                var detailsBrush = new SolidBrush(Color.FromArgb(102, 102, 102));

                foreach (string detail in storeDetails)
                {
                    var detailSize = g.MeasureString(detail, storeDetailsFont);
                    g.DrawString(detail, storeDetailsFont, detailsBrush,
                        leftMargin + (contentWidth - detailSize.Width) / 2, currentY);
                    currentY += (int)(detailSize.Height * 1.3f);
                }

                // Header border - full width
                currentY += sectionSpacing;
                g.DrawLine(new Pen(headerTextColor, 2), leftMargin, currentY, rightMargin, currentY);
                currentY += sectionSpacing;

                // INVOICE INFO SECTION - Full width layout
                string[,] invoiceInfo = {
            {"Invoice No:", invoice.InvoiceNumber.ToString()},
            {"Date:", invoice.InvoiceDate.ToString("dd-MMM-yyyy")},
            {"Customer:", invoice.CustomerName ?? "N/A"},
            {"Mobile:", invoice.CustomerMobile ?? "N/A"},
            {"GSTIN:", invoice.CustomerGSTIN ?? "N/A"},
            {"Payment:", invoice.PaymentType ?? "N/A"}
        };

                for (int i = 0; i < invoiceInfo.GetLength(0); i++)
                {
                    g.DrawString(invoiceInfo[i, 0], infoLabelFont, new SolidBrush(Color.Black), leftMargin, currentY);

                    var valueSize = g.MeasureString(invoiceInfo[i, 1], infoValueFont);
                    g.DrawString(invoiceInfo[i, 1], infoValueFont, new SolidBrush(Color.Black),
                        rightMargin - valueSize.Width, currentY);

                    currentY += lineHeight;
                }

                currentY += sectionSpacing;

                // ITEMS TABLE - Full width
                float[] columnPercents = { 0.08f, 0.40f, 0.13f, 0.13f, 0.13f, 0.13f };
                int[] columnWidths = new int[columnPercents.Length];
                int[] columnX = new int[columnPercents.Length];

                columnX[0] = leftMargin;
                for (int i = 0; i < columnPercents.Length; i++)
                {
                    columnWidths[i] = (int)(contentWidth * columnPercents[i]);
                    if (i > 0) columnX[i] = columnX[i - 1] + columnWidths[i - 1];
                }

                // Table header - spans full content width
                var headerRect = new Rectangle(leftMargin, currentY, contentWidth, rowHeight);
                g.FillRectangle(new SolidBrush(tableHeaderColor), headerRect);
                g.DrawRectangle(new Pen(tableBorderColor, 1), headerRect);

                string[] headers = { "No.", "Item", "Qty", "MRP", "Rate", "Total" };
                for (int i = 0; i < headers.Length; i++)
                {
                    var headerTextRect = new Rectangle(columnX[i], currentY, columnWidths[i], rowHeight);
                    var sf = new StringFormat()
                    {
                        Alignment = i == 1 ? StringAlignment.Near : StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };

                    if (i == 1) // Item column padding
                    {
                        headerTextRect.X += 3;
                        headerTextRect.Width -= 6;
                    }

                    g.DrawString(headers[i], tableHeaderFont, new SolidBrush(Color.Black), headerTextRect, sf);

                    // Vertical borders
                    if (i < headers.Length - 1)
                    {
                        g.DrawLine(new Pen(tableBorderColor, 1),
                                  columnX[i] + columnWidths[i], currentY,
                                  columnX[i] + columnWidths[i], currentY + rowHeight);
                    }
                }

                currentY += rowHeight;

                // Table rows - full width
                if (invoice.Items != null && invoice.Items.Count > 0)
                {
                    for (int i = 0; i < invoice.Items.Count; i++)
                    {
                        var item = invoice.Items[i];
                        if (item != null)
                        {
                            var rowRect = new Rectangle(leftMargin, currentY, contentWidth, rowHeight);

                            // Alternating row colors
                            if (i % 2 == 1)
                            {
                                g.FillRectangle(new SolidBrush(Color.FromArgb(249, 249, 249)), rowRect);
                            }

                            g.DrawRectangle(new Pen(tableBorderColor, 1), rowRect);

                            string[] values = {
                        (i + 1).ToString(),
                        item.Name ?? "",
                        item.Quantity.ToString("F2"),
                        item.MRP.ToString("F2"),
                        item.Rate.ToString("F2"),
                        item.Total.ToString("F2")
                    };

                            for (int j = 0; j < values.Length; j++)
                            {
                                var cellRect = new Rectangle(columnX[j], currentY, columnWidths[j], rowHeight);
                                var sf = new StringFormat()
                                {
                                    Alignment = j == 1 ? StringAlignment.Near : StringAlignment.Center,
                                    LineAlignment = StringAlignment.Center,
                                    Trimming = StringTrimming.EllipsisCharacter
                                };

                                if (j == 1) // Item name padding
                                {
                                    cellRect.X += 3;
                                    cellRect.Width -= 6;
                                }

                                g.DrawString(values[j], tableCellFont, new SolidBrush(Color.Black), cellRect, sf);

                                // Vertical borders
                                if (j < values.Length - 1)
                                {
                                    g.DrawLine(new Pen(tableBorderColor, 1),
                                              columnX[j] + columnWidths[j], currentY,
                                              columnX[j] + columnWidths[j], currentY + rowHeight);
                                }
                            }

                            currentY += rowHeight;
                        }
                    }
                }

                currentY += sectionSpacing;

                // TOTALS SECTION - Full width
                string[,] totals = {
            {"Total Qty:", invoice.TotalQuantity.ToString("F2")},
            {"Net Total:", $"₹{invoice.NetTotal:F2}"},
            {"Taxable:", $"₹{invoice.TaxableAmount:F2}"},
            {"CGST (2.5%):", $"₹{invoice.CGST:F2}"},
            {"SGST (2.5%):", $"₹{invoice.SGST:F2}"}
        };

                for (int i = 0; i < totals.GetLength(0); i++)
                {
                    g.DrawString(totals[i, 0], totalRowFont, new SolidBrush(Color.Black), leftMargin, currentY);

                    var valueSize = g.MeasureString(totals[i, 1], totalRowFont);
                    g.DrawString(totals[i, 1], totalRowFont, new SolidBrush(Color.Black),
                        rightMargin - valueSize.Width, currentY);

                    currentY += lineHeight;
                }

                currentY += sectionSpacing;

                // SAVINGS SECTION - Full width
                int savingsHeight = (int)(savingsFontSize * 2.5f);
                var savingsRect = new Rectangle(leftMargin, currentY, contentWidth, savingsHeight);
                g.FillRectangle(new SolidBrush(savingsColor), savingsRect);
                g.DrawRectangle(new Pen(savingsBorderColor, 1), savingsRect);

                string savingsText = $"YOU HAVE SAVED Rs. {invoice.TotalSavings:F2}";
                var savingsSize = g.MeasureString(savingsText, savingsFont);
                g.DrawString(savingsText, savingsFont, new SolidBrush(Color.Black),
                    leftMargin + (contentWidth - savingsSize.Width) / 2,
                    currentY + (savingsHeight - savingsSize.Height) / 2);

                currentY += savingsHeight + sectionSpacing;

                // BILL AMOUNT SECTION - Full width
                int billHeight = (int)(billAmountFontSize * 2.5f);
                var billRect = new Rectangle(leftMargin, currentY, contentWidth, billHeight);
                g.FillRectangle(new SolidBrush(billAmountColor), billRect);
                g.DrawRectangle(new Pen(billAmountBorderColor, 2), billRect);

                string billText = $"BILL AMOUNT: ₹{invoice.BillAmount:F2}";
                var billSize = g.MeasureString(billText, billAmountFont);
                g.DrawString(billText, billAmountFont, new SolidBrush(Color.Black),
                    leftMargin + (contentWidth - billSize.Width) / 2,
                    currentY + (billHeight - billSize.Height) / 2);

                currentY += billHeight + sectionSpacing;

                // AMOUNT IN WORDS - Full width
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

                string wordsText = $"Amount in Words: {amountInWords} Rupees Only";

                // Word wrapping - use full content width
                var words = wordsText.Split(' ');
                string currentLine = "";

                foreach (string word in words)
                {
                    string testLine = string.IsNullOrEmpty(currentLine) ? word : currentLine + " " + word;
                    var testSize = g.MeasureString(testLine, amountWordsFont);

                    if (testSize.Width <= contentWidth)
                    {
                        currentLine = testLine;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(currentLine))
                        {
                            var lineSize = g.MeasureString(currentLine, amountWordsFont);
                            g.DrawString(currentLine, amountWordsFont, new SolidBrush(Color.Black),
                                leftMargin + (contentWidth - lineSize.Width) / 2, currentY);
                            currentY += (int)(lineSize.Height * 1.3f);
                        }
                        currentLine = word;
                    }
                }

                if (!string.IsNullOrEmpty(currentLine))
                {
                    var lineSize = g.MeasureString(currentLine, amountWordsFont);
                    g.DrawString(currentLine, amountWordsFont, new SolidBrush(Color.Black),
                        leftMargin + (contentWidth - lineSize.Width) / 2, currentY);
                    currentY += (int)(lineSize.Height * 1.3f) + sectionSpacing;
                }

                // FOOTER - Full width
                g.DrawLine(new Pen(tableBorderColor, 1), leftMargin, currentY, rightMargin, currentY);
                currentY += sectionSpacing;

                string thankYouText = "Thank You & Visit Again";
                var thankYouSize = g.MeasureString(thankYouText, footerMainFont);
                g.DrawString(thankYouText, footerMainFont, new SolidBrush(Color.Black),
                    leftMargin + (contentWidth - thankYouSize.Width) / 2, currentY);

                currentY += (int)thankYouSize.Height + (lineHeight / 2);

                string printDate = $"Printed on: {DateTime.Now:dd-MMM-yyyy HH:mm}";
                var printSize = g.MeasureString(printDate, footerSmallFont);
                g.DrawString(printDate, footerSmallFont, new SolidBrush(footerColor),
                    leftMargin + (contentWidth - printSize.Width) / 2, currentY);

                // Dispose resources
                titleFont.Dispose();
                storeDetailsFont.Dispose();
                infoLabelFont.Dispose();
                infoValueFont.Dispose();
                tableHeaderFont.Dispose();
                tableCellFont.Dispose();
                totalRowFont.Dispose();
                savingsFont.Dispose();
                billAmountFont.Dispose();
                amountWordsFont.Dispose();
                footerMainFont.Dispose();
                footerSmallFont.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error drawing invoice: {ex.Message}", ex);
            }
        }
    }
}