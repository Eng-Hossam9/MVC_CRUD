using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Hossam.DAL.Model
{
    public class Departments : BaseEntity
    {
        [Required (ErrorMessage ="Name is Requierd")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Code is Requierd")]

        public string Code { get; set; }
        public DateTime DateOfCreation { get; set; }
        
        public ICollection<Employees>? Employees { get; set; }
         

    }
}
