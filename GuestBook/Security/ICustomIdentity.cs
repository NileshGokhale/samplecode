using System.Security.Principal;

namespace GuestBook.Security
{
    /// <summary>
    /// Interface used for custom principal
    /// </summary>
    interface ICustomIdentity : IPrincipal
    {
        int UserId { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
    }
}
