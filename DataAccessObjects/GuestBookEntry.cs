using System;

namespace DataAccessObjects
{
    /// <summary>
    /// Not used anywhere
    /// </summary>
    public class GuestBookEntry : MongoEntityBase
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }
        /// <summary>
        /// Gets or sets the date added.
        /// </summary>
        /// <value>
        /// The date added.
        /// </value>
        public DateTime DateAdded { get; set; }

    }
}
