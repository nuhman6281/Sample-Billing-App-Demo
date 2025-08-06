using System.Collections.ObjectModel;

namespace Sample_Billing_App.Models
{
    public class Invoice
    {
        public int InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; } = DateTime.Now;
        public TimeSpan InvoiceTime { get; set; } = DateTime.Now.TimeOfDay;
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerMobile { get; set; } = string.Empty;
        public string CustomerGSTIN { get; set; } = string.Empty;
        public string PaymentType { get; set; } = "CASH";
        public ObservableCollection<InvoiceItem> Items { get; set; } = new();
        
        public decimal TotalQuantity => Items.Sum(item => item.Quantity);
        public decimal NetTotal => Items.Sum(item => item.Total);
        public decimal TotalSavings => Items.Sum(item => item.Savings);
        public decimal TaxableAmount => NetTotal / 1.05m; // Assuming 5% GST
        public decimal CGST => TaxableAmount * 0.025m; // 2.5%
        public decimal SGST => TaxableAmount * 0.025m; // 2.5%
        public decimal BillAmount => NetTotal;
        
        public string AmountInWords 
        { 
            get 
            {
                try
                {
                    return ConvertToWords(BillAmount);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error in AmountInWords property: {ex.Message}");
                    return $"Amount: {BillAmount:F2}";
                }
            }
        }
        
        private static string ConvertToWords(decimal amount)
        {
            try
            {
                // Handle zero amount
                if (amount == 0) return "Zero";
                
                // Handle negative numbers
                if (amount < 0) return "Negative " + ConvertToWords(Math.Abs(amount));
                
                // Convert to integer part only (ignore decimal places for now)
                // Use Math.Floor and explicit conversion to avoid precision issues
                decimal flooredAmount = Math.Floor(amount);
                
                // Additional safety check for very large numbers
                if (flooredAmount > int.MaxValue)
                {
                    return $"Amount: {amount:F2}";
                }
                
                int number = (int)flooredAmount;
                
                if (number == 0) return "Zero";
                
                // Validate number is positive
                if (number < 0) return "Zero";
                
                string[] ones = { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
                string[] teens = { "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
                string[] tens = { "", "", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
                
                if (number < 10) return ones[number];
                if (number < 20) return teens[number - 10];
                if (number < 100) return tens[number / 10] + (number % 10 > 0 ? " " + ones[number % 10] : "");
                if (number < 1000) return ones[number / 100] + " Hundred" + (number % 100 > 0 ? " and " + ConvertToWords(number % 100) : "");
                if (number < 100000) return ConvertToWords(number / 1000) + " Thousand" + (number % 1000 > 0 ? " " + ConvertToWords(number % 1000) : "");
                if (number < 10000000) return ConvertToWords(number / 100000) + " Lakh" + (number % 100000 > 0 ? " " + ConvertToWords(number % 100000) : "");
                return ConvertToWords(number / 10000000) + " Crore" + (number % 10000000 > 0 ? " " + ConvertToWords(number % 10000000) : "");
            }
            catch (Exception ex)
            {
                // Return a safe fallback if conversion fails
                return $"Amount: {amount:F2}";
            }
        }
    }
} 