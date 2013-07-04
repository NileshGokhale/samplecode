
using MongoDB.Bson.Serialization.Attributes;
namespace DTO
{
    public class User : MongoEntityBase
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string ResetPasswordLink { get; set; }
        public Blog Blog { get; set; }
    }
}
