using System.ComponentModel.DataAnnotations;

namespace Company.Hossam.PL.ViewModels
{
    public class ForgetPasswordViewmodel
    {

        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress]

        public string Email { get; set; }
    }
}
