using ASAssignment3.Entities;
using ASAssignment3.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASAssignment3.Controllers
{
    public class VendorsController : Controller
    {
        public VendorsController(VendorsContext vendorDBContext)
        {
            _vendorsDbContext = vendorDBContext;
        }

        [HttpGet]
        public IActionResult AddVendors()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddVendors(VendorViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _vendorsDbContext.Vendors.Add(viewModel.CurrentVendor);
                _vendorsDbContext.SaveChanges();
                return RedirectToAction("Vendor", "Vendors");
            }
            else
            {
                viewModel.Vendors = _vendorsDbContext.Vendors.OrderBy(v => v.Name).ToList();
                return View(viewModel);
            }
        }

        [HttpGet]
        public IActionResult Vendor(string alphabeticalGroupFilter = "A B C D E")
        {
            var alphabeticalGroupList = alphabeticalGroupFilter.Split(" ");

            List<List<Vendor>> listVendors = new List<List<Vendor>>();

            List<Vendor> vendor = new List<Vendor>();

            for (int i = 0; i < alphabeticalGroupList.Length; i++)
            {
                vendor = _vendorsDbContext.Vendors.OrderBy(v => v.Name).

                Where(v => v.Name.StartsWith(alphabeticalGroupList[i])).ToList();

                if (vendor.Count > 0)
                {
                    listVendors.Add(vendor);
                }
            }
            return View(listVendors);
        }

        [HttpGet]
        public IActionResult EditVendors(int id)
        {
            var currentVendor = _vendorsDbContext.Vendors.Find(id);
            var viewModel = new VendorViewModel()
            {
                CurrentVendor = currentVendor
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult EditVendors(VendorViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _vendorsDbContext.Vendors.Update(viewModel.CurrentVendor);
                _vendorsDbContext.SaveChanges();
                return RedirectToAction("Vendor", "Vendors");
            }
            else
            {
                viewModel.Vendors = _vendorsDbContext.Vendors.OrderBy(v => v.Name).ToList();
                return View(viewModel);
            }
        }

        public IActionResult SoftDeleteVendor(int id)
        {
            var currentVendor = _vendorsDbContext.Vendors.Find(id);
            TempData["VendorDeleted"] = currentVendor.Name;
            TempData["DeletedID"] = currentVendor.VendorId;

            currentVendor.IsDeleted = true;
            _vendorsDbContext.Update(currentVendor);
            _vendorsDbContext.SaveChanges();

            return RedirectToAction("Vendor");
        }

        public IActionResult VendorUndo(int id)
        {
            var currentVendor = _vendorsDbContext.Vendors.Find(id);
            TempData["VendorDeleted"] = null;
            TempData["DeletedID"] = null;

            currentVendor.IsDeleted = false;
            _vendorsDbContext.Update(currentVendor);
            _vendorsDbContext.SaveChanges();

            return RedirectToAction("Vendor");
        }

        private VendorsContext _vendorsDbContext;
    }
}
