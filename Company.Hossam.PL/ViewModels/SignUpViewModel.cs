using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Company.Hossam.PL.ViewModels
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage ="Username is Required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "FirstName is Required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is Required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "ConfirmPassword is Required")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password),ErrorMessage ="Not matched")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress]

        public string Email { get; set; }
        [Required(ErrorMessage = "IsAgree is Required")]

        public bool IsAgree{ get; set; }
    }
}
