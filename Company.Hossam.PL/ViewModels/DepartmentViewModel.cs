using Company.Hossam.DAL.Model;
using System.ComponentModel.DataAnnotations;

namespace Company.Hossam.PL.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is Requierd")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Code is Requierd")]

        public string Code { get; set; }
        public DateTime DateOfCreation { get; set; }

        public ICollection<Employees>? Employees { get; set; }

    }
}
