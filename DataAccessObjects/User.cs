namespace DataAccessObjects
{
    /// <summary>
    /// POCO entity for Users
    /// </summary>
    public class User : MongoEntityBase
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }
        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets the reset password link.
        /// </summary>
        /// <value>
        /// The reset password link.
        /// </value>
        public string ResetPasswordLink { get; set; }
        /// <summary>
        /// Gets or sets the blog.
        /// </summary>
        /// <value>
        /// The blog.
        /// </value>
        public Blog Blog { get; set; }
    }
}
