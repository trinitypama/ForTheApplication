using System.ComponentModel.DataAnnotations;
namespace ForTheApplication.Models
{
    public class UserModel
    {
        [Key]
        public virtual string Email { get; set; }

        public virtual string Password { get; set; }

        public virtual string PhoneNumber { get; set; }

    }
}
