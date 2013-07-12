using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GuestBook.Models
{
    /// <summary>
    /// View model for Login view
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        [Required(ErrorMessage="User name is required")]
        [DisplayName("User Name")]
        public string UserName { get; set; }
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [Required(ErrorMessage = "Password is required")]
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}