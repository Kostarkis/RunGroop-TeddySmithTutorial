using System.ComponentModel.DataAnnotations;

namespace RunGroopWebApp.Dtos
{
    public class LoginAccDto
    {
        [Display(Name = "Email Address")]
        [Required(ErrorMessage ="Email message is required")]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
