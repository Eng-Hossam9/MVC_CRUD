using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Hossam.DAL.Model
{
    public class Employees : BaseEntity
    {

        [Required(ErrorMessage ="Name Is Required")]
        public string Name { get; set; }

        [Range(18,50,ErrorMessage = "Age Must be Between 18 and 50")]
        public int? Age { get; set; }

        [RegularExpression(@"[0-9]{1,3}-[a-zA-Z]{3,15}-[a-zA-Z]{3,15}-[a-zA-Z]{3,15}",
                           ErrorMessage ="Address Must Be Like 123-Street-City-Country")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Salary Is Required")]
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [EmailAddress(ErrorMessage = "The Email Address Not Valid ")]

        public string Email { get; set; }

        [Phone(ErrorMessage = "Not Valid Phone Number ")]

        public string PhoneNumber { get; set; }

        public bool IsActive { get; set; }

        public DateTime? HiringDate { get; set; }
        public DateTime? DateOfCreation { get; set; }= DateTime.Now;

        public int ? WorkForId { get; set; }
        public Departments? WorkFor { get; set; }





    }
}
