using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForTheApplication.Models.ViewModels
{
    public class UserLoginModel : UserModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Email should be a valid address.")]
        [Display(Name = "Email")]
        public override string Email { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public override string Password { get; set; }
    }
}
