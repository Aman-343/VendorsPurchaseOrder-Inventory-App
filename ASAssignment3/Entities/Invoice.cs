namespace ASAssignment3.Entities
{
    public class Invoice
    {
        public int InvoiceId { get; set; }

        public DateTime? InvoiceDate { get; set; }

        public DateTime? invoiceDueDate;

        public DateTime? InvoiceDueDate
        {
            get
            {
                DateTime dateTime = invoiceDueDate ?? DateTime.Today;
                return dateTime.AddDays(Convert.ToDouble(PaymentTerms?.DueDays));
            }
        }

        public double? PaymentTotal { get; set; } = 0.0;

        public DateTime? PaymentDate { get; set; }

        // FK:
        public int PaymentTermsId { get; set; }

        // Nav to terms:
        public PaymentTerms? PaymentTerms { get; set; }

        // FK:
        public int VendorId { get; set; }

        // Nav to vendor
        public Vendor? Vendor { get; set; }

        // Nav to line items:
        public ICollection<InvoiceLineItem>? InvoiceLineItems { get; set; }
    }
}
