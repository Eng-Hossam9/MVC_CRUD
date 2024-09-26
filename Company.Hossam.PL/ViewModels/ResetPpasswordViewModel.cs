using System.ComponentModel.DataAnnotations;

namespace Company.Hossam.PL.ViewModels
{
    public class ResetPpasswordViewModel
    {

        [Required(ErrorMessage = "password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "ConfirmPassword is Required")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Not matched")]
        public string ConfirmPassword { get; set; }
    }
}
