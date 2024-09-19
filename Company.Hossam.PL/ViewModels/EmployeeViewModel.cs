using Company.Hossam.DAL.Model;
using System.ComponentModel.DataAnnotations;

namespace Company.Hossam.PL.ViewModels
{
    public class EmployeeViewModel

    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name Is Required")]
        public string Name { get; set; }

        [Range(18, 50, ErrorMessage = "Age Must be Between 18 and 50")]
        public int? Age { get; set; }

        [RegularExpression(@"[0-9]{1,3}-[a-zA-Z]{3,15}-[a-zA-Z]{3,15}-[a-zA-Z]{3,15}",
                           ErrorMessage = "Address Must Be Like 123-Street-City-Country")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Salary Is Required")]
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [EmailAddress(ErrorMessage = "The Email Address Not Valid ")]

        public string Email { get; set; }

        [Phone(ErrorMessage = "Not Valid Phone Number ")]

        public string PhoneNumber { get; set; }


        public DateTime? HiringDate { get; set; }

        public int? WorkForId { get; set; }

        public Departments? WorkFor { get; set; }

        public IFormFile? Image { get; set; }

        public string? ImageName { get; set; }
    }
}
