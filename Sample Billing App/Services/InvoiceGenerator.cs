using Sample_Billing_App.Models;

namespace Sample_Billing_App.Services
{
    public class InvoiceGenerator
    {
        public static string GenerateInvoiceHtml(Invoice invoice, string storeName = "TUKZO ABC")
        {
            var itemsHtml = "";
            for (int i = 0; i < invoice.Items.Count; i++)
            {
                var item = invoice.Items[i];
                itemsHtml += $@"
        <div class=""item-row"">
            <div class=""col-no"">{i + 1}</div>
            <div class=""col-item item-name"">{item.Name}<br>{item.Description}</div>
            <div class=""col-qty"">{item.Quantity:F3}</div>
            <div class=""col-mrp"">{item.MRP:F2}</div>
            <div class=""col-rate"">{item.Rate:F2}</div>
            <div class=""col-total"">{item.Total:F2}</div>
        </div>";
            }

            return $@"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>{storeName} Invoice</title>
    <style>
        body {{
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 20px;
            background-color: #f5f5f5;
        }}
        
        .invoice-container {{
            width: 400px;
            margin: 0 auto;
            background-color: white;
            border: 2px solid black;
            font-size: 12px;
        }}
        
        .header {{
            text-align: center;
            padding: 10px;
            border-bottom: 2px solid black;
        }}
        
        .store-name {{
            font-size: 18px;
            font-weight: bold;
            margin-bottom: 5px;
        }}
        
        .store-details {{
            padding: 10px;
            text-align: center;
            border-bottom: 2px solid black;
            line-height: 1.4;
        }}
        
        .invoice-info {{
            padding: 10px;
            border-bottom: 2px solid black;
        }}
        
        .invoice-row {{
            display: flex;
            justify-content: space-between;
            margin-bottom: 5px;
        }}
        
        .items-header {{
            display: flex;
            background-color: #f0f0f0;
            font-weight: bold;
            padding: 5px;
            border-bottom: 1px solid black;
        }}
        
        .items-header > div {{
            text-align: center;
        }}
        
        .col-no {{ width: 30px; }}
        .col-item {{ width: 150px; text-align: left !important; }}
        .col-qty {{ width: 50px; }}
        .col-mrp {{ width: 50px; }}
        .col-rate {{ width: 50px; }}
        .col-total {{ width: 60px; }}
        
        .item-row {{
            display: flex;
            padding: 5px;
            border-bottom: 1px solid #ccc;
        }}
        
        .item-row > div {{
            text-align: center;
        }}
        
        .item-name {{
            text-align: left !important;
        }}
        
        .totals {{
            padding: 10px;
            border-bottom: 2px solid black;
        }}
        
        .total-row {{
            display: flex;
            justify-content: space-between;
            font-weight: bold;
            font-size: 14px;
            margin-bottom: 10px;
        }}
        
        .savings {{
            text-align: center;
            font-weight: bold;
            background-color: #f0f0f0;
            padding: 5px;
            margin-bottom: 10px;
        }}
        
        .tax-table {{
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 10px;
        }}
        
        .tax-table th, .tax-table td {{
            border: 1px solid black;
            padding: 5px;
            text-align: center;
        }}
        
        .tax-table th {{
            background-color: #f0f0f0;
        }}
        
        .amount-words {{
            text-align: center;
            margin-bottom: 10px;
            font-size: 11px;
        }}
        
        .bill-amount {{
            text-align: center;
            font-size: 16px;
            font-weight: bold;
            border: 2px solid black;
            padding: 10px;
            margin-bottom: 15px;
        }}
        
        .payment-info {{
            display: flex;
            justify-content: space-between;
            margin-bottom: 20px;
        }}
        
        .footer {{
            display: flex;
            justify-content: space-between;
            padding: 10px;
            font-size: 10px;
        }}
        
        .thank-you {{
            text-align: center;
            font-weight: bold;
            padding: 5px;
            border-top: 1px solid black;
            margin-top: 10px;
        }}
        
        hr {{
            border: none;
            border-top: 1px solid black;
            margin: 10px 0;
        }}
    </style>
</head>
<body>
    <div class=""invoice-container"">
        <!-- Header -->
        <div class=""header"">
            <div class=""store-name"">{storeName.ToUpper()}</div>
        </div>
        
        <!-- Store Details -->
        <div class=""store-details"">
            MUNICIPAL SHOPPING COMPLEX,NGO<br>
            QUARTERS,KAKKANAD<br>
            PIN:682021,PH.9995379212<br>
            GSTIN: 32CVPPM1824A1ZY<br>
            <strong>Goods &services Tax (GST)Act 2017</strong><br>
            <strong>RETAIL INVOICE</strong>
        </div>
        
        <!-- Invoice Info -->
        <div class=""invoice-info"">
            <div class=""invoice-row"">
                <span><strong>Inv No :</strong> {invoice.InvoiceNumber}</span>
                <span><strong>Date :</strong> {invoice.InvoiceDate:dd-MMM-yyyy}</span>
            </div>
            <div class=""invoice-row"">
                <span><strong>Name :</strong> {invoice.CustomerName}</span>
                <span><strong>Inv Time :</strong> {invoice.InvoiceTime:hh:mm tt}</span>
            </div>
            <div class=""invoice-row"">
                <span><strong>Mob :</strong> {invoice.CustomerMobile}</span>
                <span><strong>GSTIN :</strong> {invoice.CustomerGSTIN}</span>
            </div>
        </div>
        
        <!-- Items Header -->
        <div class=""items-header"">
            <div class=""col-no"">No.</div>
            <div class=""col-item"">Item Name</div>
            <div class=""col-qty"">Qty</div>
            <div class=""col-mrp"">MRP</div>
            <div class=""col-rate"">Rate</div>
            <div class=""col-total"">Total</div>
        </div>
        
        <!-- Items -->
{itemsHtml}
        
        <!-- Totals -->
        <div class=""totals"">
            <div class=""total-row"">
                <span>Total Qty : {invoice.TotalQuantity:F3}</span>
                <span>Net Total : {invoice.NetTotal:F2}</span>
            </div>
            
            <div class=""savings"">
                YOU HAVE SAVED Rs. {invoice.TotalSavings:F2}
            </div>
            
            <table class=""tax-table"">
                <tr>
                    <th>Tax%</th>
                    <th>Taxable</th>
                    <th>CGST</th>
                    <th>SGST</th>
                </tr>
                <tr>
                    <td>5.00</td>
                    <td>{invoice.TaxableAmount:F2}</td>
                    <td>{invoice.CGST:F2}</td>
                    <td>{invoice.SGST:F2}</td>
                </tr>
            </table>
        </div>
        
        <div class=""amount-words"">
            {invoice.AmountInWords} Rupees Zero Paisa Only
        </div>
        
        <div class=""bill-amount"">
            BILL AMOUNT : {invoice.BillAmount:F2}
        </div>
        
        <div class=""payment-info"">
            <span><strong>Payment Type :</strong></span>
            <span><strong>Amount :</strong></span>
        </div>
        
        <div class=""payment-info"">
            <span>{invoice.PaymentType}</span>
            <span>{invoice.BillAmount:F2}</span>
        </div>
        
        <div class=""footer"">
            <span>User : ADMIN</span>
            <span>{Environment.MachineName}</span>
        </div>
        
        <div class=""thank-you"">
            ===========Thank You & Visit Again===========
        </div>
    </div>
</body>
</html>";
        }
    }
} 