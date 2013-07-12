using MongoDB.Bson;

namespace DataAccessObjects
{
    /// <summary>
    /// Base entity class
    /// </summary>
    public abstract class MongoEntityBase
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        public ObjectId Id { get; set; }
        /// <summary>
        /// Gets or sets the entity id.
        /// </summary>
        /// <value>
        /// The entity id.
        /// </value>
        public int EntityId { get; set; }
    }
}
