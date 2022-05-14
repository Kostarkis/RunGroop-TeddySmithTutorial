using System.ComponentModel.DataAnnotations;

namespace RunGroopWebApp.Dtos
{
    public class RegisterAccDto
    {
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email message is required")]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password do not match")]
        public string ConfirmPassword { get; set; }
    }
}
