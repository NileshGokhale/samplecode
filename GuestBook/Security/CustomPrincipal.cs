using System.Security.Principal;

namespace GuestBook.Security
{
    /// <summary>
    /// Custom principal class. Object represents the current user identity
    /// </summary>
    public class CustomPrincipal : ICustomIdentity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomPrincipal"/> class.
        /// </summary>
        public CustomPrincipal()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomPrincipal"/> class.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        public CustomPrincipal(string userName)
        {
            Identity = new GenericIdentity(userName);
        }

        #region ICustomIdentity Members

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>
        /// The user id.
        /// </value>
        public int UserId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName
        {
            get;
            set;
        }

        #endregion

        #region IPrincipal Members

        /// <summary>
        /// Gets the identity of the current principal.
        /// </summary>
        /// <returns>The <see cref="T:System.Security.Principal.IIdentity" /> object associated with the current principal.</returns>
        public IIdentity Identity
        {
            get;
            private set;
        }

        /// <summary>
        /// Determines whether the current principal belongs to the specified role.
        /// </summary>
        /// <param name="role">The name of the role for which to check membership.</param>
        /// <returns>
        /// true if the current principal is a member of the specified role; otherwise, false.
        /// </returns>
        public bool IsInRole(string role)
        {
            return false;
        }

        #endregion
    }
}