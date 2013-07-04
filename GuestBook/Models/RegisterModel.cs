using Foolproof;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace GuestBook.Models
{
    public class RegisterModel
    {
        [Required(AllowEmptyStrings = false)]
        [DisplayName("User Name")]
        public string UserName { get; set; }
        [Required(AllowEmptyStrings = false)]
        [DataType( System.ComponentModel.DataAnnotations.DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }
        [EqualTo("Password")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")]
        [DisplayName("Email")]
        [Required]
        public string Email { get; set; }
    }
}