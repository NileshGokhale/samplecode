using System;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataAccessObjects
{
    /// <summary>
    /// POCO entity for BLogs
    /// </summary>
    public class Blog : MongoEntityBase
    {
        /// <summary>
        /// Gets or sets the blog id.
        /// </summary>
        /// <value>
        /// The blog id.
        /// </value>
        public int BlogId { get; set; }
        /// <summary>
        /// Gets or sets the topic.
        /// </summary>
        /// <value>
        /// The topic.
        /// </value>
        [DisplayName("Topic")]
        public string Topic { get; set; }
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        [DisplayName("Detail")]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }
        /// <summary>
        /// Gets or sets the date added.
        /// </summary>
        /// <value>
        /// The date added.
        /// </value>
        public DateTime DateAdded { get; set; }
        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        [DisplayName("Category")]
        public string Category { get; set; }
        /// <summary>
        /// Gets the year.
        /// </summary>
        /// <value>
        /// The year.
        /// </value>
        [BsonIgnore]
        public int Year { get { return DateAdded.Year; } }
        /// <summary>
        /// Gets the month.
        /// </summary>
        /// <value>
        /// The month.
        /// </value>
        [BsonIgnore]
        public int Month { get { return DateAdded.Month; } }
        /// <summary>
        /// Gets the day.
        /// </summary>
        /// <value>
        /// The day.
        /// </value>
        [BsonIgnore]
        public int Day { get { return DateAdded.Day; } }
        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>
        /// The user id.
        /// </value>
        public int UserId { get; set; }
        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public User User { get; set; }
    }
}
