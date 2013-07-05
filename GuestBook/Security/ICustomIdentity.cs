using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;

namespace GuestBook.Security
{
    interface ICustomIdentity : IPrincipal
    {
        int UserId { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
    }
}
