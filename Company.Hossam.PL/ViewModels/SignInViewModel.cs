using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Company.Hossam.PL.ViewModels
{
    public class SignInViewModel    {
      

     

        [Required(ErrorMessage = "password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress]

        public string Email { get; set; }
        [Required(ErrorMessage = "IsAgree is Required")]


        public bool RemembeME{ get; set; }

		
  
    }
}
