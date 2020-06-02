using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForTheApplication.Models.ViewModels
{
    public class UserRegistrationModel : UserModel
    {
        [Required]
        [EmailAddress(ErrorMessage ="Email should be a valid address.")]
        [Display(Name = "Email")]
        public override string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 4)]
        public override string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        //[RegularExpression(@"/(((\(\d{3}\) ?)|(\d{3}-)|(\d{3}\.))?\d{3}(-|\.)\d{4})/g", ErrorMessage = "Wrong mobile")]
        public override string PhoneNumber { get; set; }
    }
}
