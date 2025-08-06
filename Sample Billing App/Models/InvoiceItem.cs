namespace Sample_Billing_App.Models
{
    public class InvoiceItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public decimal MRP { get; set; }
        public decimal Rate { get; set; }
        public decimal Total => Quantity * Rate;
        public decimal Savings => Quantity * (MRP - Rate);
    }
} 