using Sample_Billing_App.Models;
using Sample_Billing_App.Services;
using System.Drawing.Printing;

namespace Sample_Billing_App
{
    public partial class Form1 : Form
    {
        private Invoice _currentInvoice;
        private List<InvoiceItem> _availableItems;
        private Random _random = new Random();

        public Form1()
        {
            InitializeComponent();
            InitializeInvoice();
            LoadAvailableItems();
            SetupEventHandlers();
            // UpdatePreview(); // Remove this - WebBrowser not initialized yet
        }

        private void InitializeInvoice()
        {
            _currentInvoice = new Invoice
            {
                InvoiceNumber = 1,
                InvoiceDate = DateTime.Now,
                InvoiceTime = DateTime.Now.TimeOfDay,
                CustomerName = "",
                CustomerMobile = "",
                CustomerGSTIN = "",
                PaymentType = "CASH"
            };
        }

        private void LoadAvailableItems()
        {
            _availableItems = new List<InvoiceItem>
            {
                new InvoiceItem { Id = 1, Name = "BASMATI RICE", Description = "5KG", MRP = 450.00m, Rate = 425.00m },
                new InvoiceItem { Id = 2, Name = "SUNFLOWER OIL", Description = "1LR", MRP = 180.00m, Rate = 175.00m },
                new InvoiceItem { Id = 3, Name = "TOOR DAL", Description = "1KG", MRP = 165.00m, Rate = 160.00m },
                new InvoiceItem { Id = 4, Name = "SUGAR", Description = "1KG", MRP = 45.00m, Rate = 42.00m },
                new InvoiceItem { Id = 5, Name = "TEA POWDER", Description = "250G", MRP = 120.00m, Rate = 115.00m },
                new InvoiceItem { Id = 6, Name = "WHEAT FLOUR", Description = "2KG", MRP = 80.00m, Rate = 75.00m },
                new InvoiceItem { Id = 7, Name = "COOKING OIL", Description = "1L", MRP = 120.00m, Rate = 110.00m },
                new InvoiceItem { Id = 8, Name = "SALT", Description = "1KG", MRP = 20.00m, Rate = 18.00m },
                new InvoiceItem { Id = 9, Name = "SPICES MIX", Description = "100G", MRP = 35.00m, Rate = 32.00m },
                new InvoiceItem { Id = 10, Name = "PULSES MIX", Description = "500G", MRP = 55.00m, Rate = 50.00m }
            };

            cboItems.DataSource = _availableItems;
            cboItems.DisplayMember = "Name";
            cboItems.ValueMember = "Id";
        }

        private void SetupEventHandlers()
        {
            // Invoice number
            txtInvoiceNumber.TextChanged += (s, e) => 
            { 
                if (int.TryParse(txtInvoiceNumber.Text, out int invNumber))
                {
                    _currentInvoice.InvoiceNumber = invNumber;
                    // No need for live preview updates - PDF will be generated on demand
                }
            };

            // Customer details
            txtCustomerName.TextChanged += (s, e) => { _currentInvoice.CustomerName = txtCustomerName.Text; };
            txtCustomerMobile.TextChanged += (s, e) => { _currentInvoice.CustomerMobile = txtCustomerMobile.Text; };
            txtCustomerGSTIN.TextChanged += (s, e) => { _currentInvoice.CustomerGSTIN = txtCustomerGSTIN.Text; };
            cboPaymentType.SelectedIndexChanged += (s, e) => { _currentInvoice.PaymentType = cboPaymentType.Text; };

            // Item details
            cboItems.SelectedIndexChanged += (s, e) => UpdateItemDetails();
            numQuantity.ValueChanged += (s, e) => UpdateItemTotal();
            numMRP.ValueChanged += (s, e) => UpdateItemTotal();
            numRate.ValueChanged += (s, e) => UpdateItemTotal();

            // Buttons
            btnAddItem.Click += BtnAddItem_Click;
            btnRemoveItem.Click += BtnRemoveItem_Click;
            btnPrint.Click += BtnPrint_Click;
            btnClear.Click += BtnClear_Click;
            btnNewInvoice.Click += BtnNewInvoice_Click;
            btnLoadSampleData.Click += BtnLoadSampleData_Click;
            btnRefreshPreview.Click += BtnRefreshPreview_Click;

            // List view
            lvItems.SelectedIndexChanged += (s, e) => UpdateRemoveButtonState();
        }

        private void BtnLoadSampleData_Click(object sender, EventArgs e)
        {
            try
            {
                LoadRandomSampleData();
                UpdateItemsListView();
                
                // Auto-refresh preview
                ShowInvoicePreview();
                
                MessageBox.Show("Sample data loaded successfully! Invoice preview updated.", "Sample Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading sample data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRefreshPreview_Click(object sender, EventArgs e)
        {
            try
            {
                if (_currentInvoice.Items.Count == 0)
                {
                    MessageBox.Show("Please add at least one item before previewing.", "No Items", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validate invoice data
                if (_currentInvoice == null)
                {
                    MessageBox.Show("Invoice data is not available.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Display invoice preview directly in the right panel
                ShowInvoicePreview();
                MessageBox.Show("Invoice preview updated!", "Preview", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Show detailed error message
                string errorMessage = $"Error previewing invoice: {ex.Message}";
                if (ex.InnerException != null)
                {
                    errorMessage += $"\n\nDetails: {ex.InnerException.Message}";
                }
                
                MessageBox.Show(errorMessage, "Preview Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowInvoicePreview()
        {
            try
            {
                // Clear the preview panel
                pnlPreview.Controls.Clear();

                // Create a WebBrowser control for HTML rendering
                var previewBrowser = new WebBrowser
                {
                    Dock = DockStyle.Fill,
                    ScriptErrorsSuppressed = true,
                    AllowNavigation = false,
                    AllowWebBrowserDrop = false,
                    IsWebBrowserContextMenuEnabled = false,
                    WebBrowserShortcutsEnabled = false,
                    ScrollBarsEnabled = true,
                    ObjectForScripting = null
                };

                // Generate HTML content
                string htmlContent = GenerateInvoiceHtml(_currentInvoice);
                previewBrowser.DocumentText = htmlContent;

                // Add to the preview panel
                pnlPreview.Controls.Add(previewBrowser);
            }
            catch (Exception ex)
            {
                // Show error in preview panel
                var errorLabel = new Label
                {
                    Text = $"Error displaying preview:\n{ex.Message}",
                    Font = new Font("Segoe UI", 10),
                    ForeColor = Color.Red,
                    BackColor = Color.LightPink,
                    BorderStyle = BorderStyle.FixedSingle,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill,
                    AutoSize = false
                };
                pnlPreview.Controls.Add(errorLabel);
            }
        }

        private string GenerateInvoiceHtml(Invoice invoice)
        {
            var sb = new System.Text.StringBuilder();
            
            // Start HTML document
            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html>");
            sb.AppendLine("<head>");
            sb.AppendLine("<meta charset='utf-8'>");
            sb.AppendLine("<title>Invoice Preview</title>");
            sb.AppendLine("<style>");
            sb.AppendLine("@media print {");
            sb.AppendLine("  body { margin: 0; padding: 10px; }");
            sb.AppendLine("  .invoice-container { max-width: none; margin: 0; border: none; }");
            sb.AppendLine("  .no-print { display: none; }");
            sb.AppendLine("}");
            sb.AppendLine("body { font-family: Arial, sans-serif; margin: 20px; background-color: #f5f5f5; font-size: 14px; }");
            sb.AppendLine(".invoice-container { max-width: 600px; margin: 0 auto; background-color: white; padding: 20px; border: 1px solid #ccc; border-radius: 5px; }");
            sb.AppendLine(".header { text-align: center; border-bottom: 2px solid #333; padding-bottom: 10px; margin-bottom: 20px; }");
            sb.AppendLine(".store-name { font-size: 24px; font-weight: bold; color: #333; }");
            sb.AppendLine(".store-details { font-size: 12px; color: #666; margin-top: 5px; }");
            sb.AppendLine(".invoice-info { margin-bottom: 20px; }");
            sb.AppendLine(".info-row { display: flex; justify-content: space-between; margin-bottom: 5px; }");
            sb.AppendLine(".info-label { font-weight: bold; }");
            sb.AppendLine("table { width: 100%; border-collapse: collapse; margin: 20px 0; }");
            sb.AppendLine("th, td { border: 1px solid #ddd; padding: 8px; text-align: left; }");
            sb.AppendLine("th { background-color: #f2f2f2; font-weight: bold; }");
            sb.AppendLine(".totals { margin-top: 20px; }");
            sb.AppendLine(".total-row { display: flex; justify-content: space-between; margin-bottom: 10px; font-weight: bold; }");
            sb.AppendLine(".savings { background-color: #fff3cd; padding: 10px; text-align: center; margin: 10px 0; border: 1px solid #ffeaa7; }");
            sb.AppendLine(".bill-amount { background-color: #d1ecf1; padding: 15px; text-align: center; font-size: 18px; font-weight: bold; margin: 20px 0; border: 2px solid #bee5eb; }");
            sb.AppendLine(".footer { text-align: center; margin-top: 20px; padding-top: 10px; border-top: 1px solid #ddd; color: #666; }");
            sb.AppendLine("</style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("<div class='invoice-container'>");
            
            // Header
            sb.AppendLine("<div class='header'>");
            sb.AppendLine("<div class='store-name'>TUKZO ABC</div>");
            sb.AppendLine("<div class='store-details'>");
            sb.AppendLine("MUNICIPAL SHOPPING COMPLEX, NGO QUARTERS, KAKKANAD<br>");
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
            sb.AppendLine($"<div class='info-row'><span class='info-label'>Payment Type:</span><span>{invoice.PaymentType ?? "N/A"}</span></div>");
            sb.AppendLine("</div>");
            
            // Items table
            sb.AppendLine("<table>");
            sb.AppendLine("<thead>");
            sb.AppendLine("<tr>");
            sb.AppendLine("<th>No.</th>");
            sb.AppendLine("<th>Item Name</th>");
            sb.AppendLine("<th>Qty</th>");
            sb.AppendLine("<th>MRP</th>");
            sb.AppendLine("<th>Rate</th>");
            sb.AppendLine("<th>Total</th>");
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
                        sb.AppendLine($"<td>{item.Name ?? ""}</td>");
                        sb.AppendLine($"<td>{item.Quantity:F3}</td>");
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
            sb.AppendLine($"<div class='total-row'><span>Total Quantity:</span><span>{invoice.TotalQuantity:F3}</span></div>");
            sb.AppendLine($"<div class='total-row'><span>Net Total:</span><span>₹{invoice.NetTotal:F2}</span></div>");
            sb.AppendLine($"<div class='total-row'><span>Taxable Amount:</span><span>₹{invoice.TaxableAmount:F2}</span></div>");
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
                // Log the error for debugging
                System.Diagnostics.Debug.WriteLine($"Error converting amount to words: {ex.Message}");
                amountInWords = $"Amount: {invoice.BillAmount:F2}";
            }
            sb.AppendLine($"<div style='text-align: center; margin: 20px 0;'>Amount in Words: {amountInWords} Rupees Only</div>");
            
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

        private void LoadRandomSampleData()
        {
            // Generate random customer data
            string[] customerNames = { "John Smith", "Mary Johnson", "David Wilson", "Sarah Brown", "Michael Davis", "Lisa Anderson", "Robert Taylor", "Jennifer White", "William Miller", "Amanda Garcia" };
            string[] customerMobiles = { "9876543210", "8765432109", "7654321098", "6543210987", "5432109876", "4321098765", "3210987654", "2109876543", "1098765432", "0987654321" };
            string[] customerGSTINs = { "27AABCU9603R1Z5", "29AABCA1234A1Z5", "33AABCA5678B1Z5", "36AABCA9012C1Z5", "32AABCA3456D1Z5", "24AABCA7890E1Z5", "22AABCA2345F1Z5", "20AABCA6789G1Z5", "19AABCA0123H1Z5", "18AABCA4567I1Z5" };
            string[] paymentTypes = { "CASH", "CARD", "UPI", "CHEQUE" };

            // Random invoice number
            int randomInvoiceNumber = _random.Next(100, 9999);
            txtInvoiceNumber.Text = randomInvoiceNumber.ToString();

            // Random customer details
            txtCustomerName.Text = customerNames[_random.Next(customerNames.Length)];
            txtCustomerMobile.Text = customerMobiles[_random.Next(customerMobiles.Length)];
            txtCustomerGSTIN.Text = customerGSTINs[_random.Next(customerGSTINs.Length)];
            cboPaymentType.SelectedIndex = _random.Next(paymentTypes.Length);

            // Clear existing items
            _currentInvoice.Items.Clear();

            // Add random items (2-5 items)
            int itemCount = _random.Next(2, 6);
            for (int i = 0; i < itemCount; i++)
            {
                var randomItem = _availableItems[_random.Next(_availableItems.Count)];
                var quantity = _random.Next(1, 5) + _random.NextDouble(); // Random quantity with decimals
                var mrp = randomItem.MRP + _random.Next(-10, 11); // Random MRP variation
                var rate = mrp - _random.Next(5, 16); // Random discount

                var newItem = new InvoiceItem
                {
                    Id = i + 1,
                    Name = randomItem.Name,
                    Description = randomItem.Description,
                    Quantity = (decimal)quantity,
                    MRP = (decimal)mrp,
                    Rate = (decimal)rate
                };

                _currentInvoice.Items.Add(newItem);
            }
        }

      

        private void UpdateItemDetails()
        {
            if (cboItems.SelectedItem is InvoiceItem selectedItem)
            {
                txtItemDescription.Text = selectedItem.Description;
                numMRP.Value = selectedItem.MRP;
                numRate.Value = selectedItem.Rate;
                numQuantity.Value = 1;
                UpdateItemTotal();
            }
        }

        private void UpdateItemTotal()
        {
            try
            {
                decimal quantity = numQuantity.Value;
                decimal rate = numRate.Value;
                decimal total = quantity * rate;
                lblItemTotal.Text = $"Total: ₹{total:F2}";
            }
            catch
            {
                lblItemTotal.Text = "Total: ₹0.00";
            }
        }

        private void BtnAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboItems.SelectedItem is InvoiceItem selectedItem)
                {
                    var newItem = new InvoiceItem
                    {
                        Id = _currentInvoice.Items.Count + 1,
                        Name = selectedItem.Name,
                        Description = txtItemDescription.Text,
                        Quantity = numQuantity.Value,
                        MRP = numMRP.Value,
                        Rate = numRate.Value
                    };

                    _currentInvoice.Items.Add(newItem);
                    UpdateItemsListView();
                    ClearItemForm();
                    
                    // Auto-refresh preview
                    ShowInvoicePreview();
                }
                else
                {
                    MessageBox.Show("Please select an item first.", "No Item Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding item: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRemoveItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvItems.SelectedItems.Count > 0)
                {
                    int selectedIndex = lvItems.SelectedIndices[0];
                    if (selectedIndex >= 0 && selectedIndex < _currentInvoice.Items.Count)
                    {
                        _currentInvoice.Items.RemoveAt(selectedIndex);
                        UpdateItemsListView();
                        UpdateRemoveButtonState();
                        
                        // Auto-refresh preview
                        ShowInvoicePreview();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error removing item: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateItemsListView()
        {
            try
            {
                lvItems.Items.Clear();
                foreach (var item in _currentInvoice.Items)
                {
                    var listItem = new ListViewItem(item.Id.ToString());
                    listItem.SubItems.Add(item.Name);
                    listItem.SubItems.Add(item.Description);
                    listItem.SubItems.Add(item.Quantity.ToString("F3"));
                    listItem.SubItems.Add(item.MRP.ToString("F2"));
                    listItem.SubItems.Add(item.Rate.ToString("F2"));
                    listItem.SubItems.Add(item.Total.ToString("F2"));
                    lvItems.Items.Add(listItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating items list: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateRemoveButtonState()
        {
            btnRemoveItem.Enabled = lvItems.SelectedItems.Count > 0;
        }

        private void ClearItemForm()
        {
            cboItems.SelectedIndex = -1;
            txtItemDescription.Text = "";
            numQuantity.Value = 1;
            numMRP.Value = 0;
            numRate.Value = 0;
            lblItemTotal.Text = "Total: ₹0.00";
        }

     

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboPrinters.SelectedItem == null)
                {
                    MessageBox.Show("Please select a printer first.", "Printer Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_currentInvoice.Items.Count == 0)
                {
                    MessageBox.Show("Please add at least one item before printing.", "No Items", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Use the new PDF-based printing approach
                PdfInvoiceGenerator.PrintInvoice(_currentInvoice, cboPrinters.Text);
                MessageBox.Show("Invoice sent to printer successfully!", "Print Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error printing invoice: {ex.Message}", "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to clear all items?", "Clear Items", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _currentInvoice.Items.Clear();
                    UpdateItemsListView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error clearing items: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnNewInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to create a new invoice?", "New Invoice", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _currentInvoice.InvoiceNumber++;
                    _currentInvoice.InvoiceDate = DateTime.Now;
                    _currentInvoice.InvoiceTime = DateTime.Now.TimeOfDay;
                    _currentInvoice.Items.Clear();
                    _currentInvoice.CustomerName = "";
                    _currentInvoice.CustomerMobile = "";
                    _currentInvoice.CustomerGSTIN = "";
                    _currentInvoice.PaymentType = "CASH";

                    // Update UI
                    txtInvoiceNumber.Text = _currentInvoice.InvoiceNumber.ToString();
                    txtCustomerName.Text = "";
                    txtCustomerMobile.Text = "";
                    txtCustomerGSTIN.Text = "";
                    cboPaymentType.SelectedIndex = 0;
                    UpdateItemsListView();
                    ClearItemForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating new invoice: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            try
            {
                base.OnLoad(e);
                
                // Load printers
                foreach (string printer in PrinterSettings.InstalledPrinters)
                {
                    cboPrinters.Items.Add(printer);
                }
                if (cboPrinters.Items.Count > 0)
                    cboPrinters.SelectedIndex = 0;

                // Set payment types
                cboPaymentType.Items.AddRange(new string[] { "CASH", "CARD", "UPI", "CHEQUE" });
                cboPaymentType.SelectedIndex = 0;

                // Set initial invoice number
                txtInvoiceNumber.Text = _currentInvoice.InvoiceNumber.ToString();

                // Add welcome message to preview panel
                var welcomeLabel = new Label
                {
                    Text = "📄 Invoice Preview Panel\n\n" +
                           "• Enter customer details on the left\n" +
                           "• Add items to the invoice\n" +
                           "• Click 'Refresh Preview' to see the invoice\n" +
                           "• Click 'Print Invoice' to print\n\n" +
                           "The preview will appear here in real-time\n" +
                           "as you add items and update details.",
                    Font = new Font("Segoe UI", 11),
                    ForeColor = Color.DarkBlue,
                    BackColor = Color.LightCyan,
                    BorderStyle = BorderStyle.FixedSingle,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill,
                    AutoSize = false
                };
                
                pnlPreview.Controls.Add(welcomeLabel);
                
                System.Diagnostics.Debug.WriteLine("Form loaded successfully with PDF-based preview system");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing application: {ex.Message}", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
