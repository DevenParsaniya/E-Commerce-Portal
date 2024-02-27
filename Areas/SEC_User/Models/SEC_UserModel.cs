using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Website.Areas.SEC_User.Models
{
    public class SEC_UserModel
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "User Name is required")]
        [DisplayName("User Name")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DisplayName("Password")]
        public string Password { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string? LastName { get; set; }

        [DisplayName("MobileNumber")]
        public string? MobileNumber { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [DisplayName("Email Address")]
        public string? UserEmail { get; set; }

        [DisplayName("Profile Picture")]
        public IFormFile? ProfileImage { get; set; }

        [DisplayName("Is Admin")]
        public bool? IsAdmin { get; set; }
        [DisplayName("Is Active")]
        public bool? IsActive { get; set; }

        [ScaffoldColumn(false)] // Hide these fields from scaffolding
        public DateTime CreatedAt { get; set; }

        [ScaffoldColumn(false)] // Hide these fields from scaffolding
        public DateTime ModifiedAt { get; set; }
    }
    public class UserLoginModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }

}
