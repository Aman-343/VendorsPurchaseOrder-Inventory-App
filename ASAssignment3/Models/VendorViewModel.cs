using ASAssignment3.Entities;

namespace ASAssignment3.Models
{
    public class VendorViewModel
    {
        public List<Vendor>? Vendors { get; set; }
        public Vendor CurrentVendor { get; set; }
        public Vendor? Vendor { get; set; }
        public List<Invoice>? Invoices { get; set; }
        public Invoice? Invoice { get; set; }
        public InvoiceLineItem? InvoiceLineItem { get; set; }
        public List<InvoiceLineItem>? InvoiceItems { get; set; }
        public List<PaymentTerms>? AllPaymentTerms { get; set; }
        public PaymentTerms? PaymentTerms { get; set; }
    }
}
