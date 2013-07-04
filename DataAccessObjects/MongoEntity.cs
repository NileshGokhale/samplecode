using MongoDB.Bson;

namespace DTO
{
    public class MongoEntityBase
    {
        public ObjectId Id { get; set; }
        public int EntityId { get; set; }
    }
}
