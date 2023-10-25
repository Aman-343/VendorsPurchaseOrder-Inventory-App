namespace ASAssignment3.Entities
{
    public class InvoiceLineItem
    {
        public int InvoiceLineItemId { get; set; }

        public double? Amount { get; set; }

        public string? Description { get; set; }

        public int? InvoiceId { get; set; }

        public Invoice? Invoice { get; set; }
    }
}
