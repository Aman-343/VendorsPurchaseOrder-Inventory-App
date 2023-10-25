using ASAssignment3.Entities;
using ASAssignment3.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASAssignment3.Controllers
{
    public class InvoiceController : Controller
    {
        public InvoiceController(VendorsContext vendorDBContext)
        {
            _vendorsDbContext = vendorDBContext;
        }
        [HttpGet]
        public IActionResult InvoiceVendors(int vendorId, int invoiceId, int paymentTermsId)
        {
            var vendor = _vendorsDbContext.Vendors.Find(vendorId);

            List<Invoice> invoices = new List<Invoice>();

            if (invoiceId == 0)
            {
                invoices = _vendorsDbContext.Invoices.Where(v => v.VendorId == vendorId).ToList();
            }

            var invoiceList = _vendorsDbContext.Invoices.OrderBy(inv => inv.InvoiceId).Where(inv => inv.VendorId == vendorId).ToList();

            var invoiceLineItemList = _vendorsDbContext.InvoiceLineItems.Where(ili => ili.InvoiceId == invoiceId).ToList();

            var paymentTerms = _vendorsDbContext.TermPayments.Where(pt => pt.PaymentTermsId == paymentTermsId).FirstOrDefault();

            var paymentTermList = _vendorsDbContext.TermPayments.ToList();

            var invoice = _vendorsDbContext.Invoices.Where(inv => inv.InvoiceId == invoiceId).FirstOrDefault();

            var vendorViewModel = new VendorViewModel()
            {
                CurrentVendor = vendor,
                Invoices = invoiceList,
                InvoiceItems = invoiceLineItemList,
                PaymentTerms = paymentTerms,
                AllPaymentTerms = paymentTermList,
                Invoice = invoice
            };

            return View(vendorViewModel);
        }

        [HttpPost]
        public IActionResult InvoiceAdd(int vendorId, VendorViewModel viewModel)
        {
            _vendorsDbContext.Invoices.Add(viewModel.Invoice);
            _vendorsDbContext.SaveChanges();
            return RedirectToAction("InvoiceVendors", new { vendorId = vendorId });
        }


        [HttpPost]
        public IActionResult InvoiceLineItemAdd(int termID, VendorViewModel viewModel)
        {
            int? invoiceId = viewModel.InvoiceLineItem.InvoiceId;

            int vendorId = viewModel.Vendor.VendorId;

            _vendorsDbContext.InvoiceLineItems.Add(viewModel.InvoiceLineItem);

            _vendorsDbContext.SaveChanges();

            return RedirectToAction("InvoiceVendors", new { vendorId = vendorId, invoiceId = invoiceId, paymentTermsId = termID });
        }


        private VendorsContext _vendorsDbContext;
    }
}
