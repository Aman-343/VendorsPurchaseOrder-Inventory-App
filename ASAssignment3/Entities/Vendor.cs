using System.ComponentModel.DataAnnotations;

namespace ASAssignment3.Entities
{
    public class Vendor
    {
        public int VendorId { get; set; }

        [Required(ErrorMessage = "Vendor Name is required.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "First Address is required.")]
        public string? Address1 { get; set; }

        public string? Address2 { get; set; }

        [Required(ErrorMessage = "Name of the City is required.")]
        public string? City { get; set; } = null!;

        [Required(ErrorMessage = "Province/State is required.")]
        [RegularExpression(@"^[A-Za-z]{2}$", ErrorMessage ="Invalid province code.")]
        public string? ProvinceOrState { get; set; } = null!;

        [Required(ErrorMessage = "Zip/Postal Code is required.")]
        public string? ZipOrPostalCode { get; set; } = null!;

        [Required(ErrorMessage = "Vendor Phone Number is required.")]
        [Phone(ErrorMessage ="Please provide a valid number")]
        public string? VendorPhone { get; set; }

        public string? VendorContactLastName { get; set; }

        public string? VendorContactFirstName { get; set; }

        [EmailAddress(ErrorMessage = "Please provide a valid email address.")]
        public string? VendorContactEmail { get; set; }

        public bool IsDeleted { get; set; } = false;

        public ICollection<Invoice>? Invoices { get; set; }
    }
}
