using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;

namespace GuestBook.Security
{
    public class CustomPrincipal : ICustomIdentity
    {
        public CustomPrincipal(string userName)
        {
            this.Identity = new GenericIdentity(userName);
        }

        #region ICustomIdentity Members

        public int UserId
        {
            get;
            set;
        }

        public string FirstName
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }

        #endregion

        #region IPrincipal Members

        public IIdentity Identity
        {
            get;
            private set;
        }

        public bool IsInRole(string role)
        {
            return false;
        }

        #endregion
    }
}