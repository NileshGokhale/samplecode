using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class Blog : MongoEntityBase
    {
        public int BlogId { get; set; }
        [DisplayName("Topic")]
        public string Topic { get; set; }
        [DisplayName("Detail")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
        public string Text { get; set; }
        [BsonDateTimeOptions(DateOnly = true)]
        public DateTime DateAdded { get; set; }
        [BsonIgnore]
        public int Year { get { return DateAdded.Year; } }
        [BsonIgnore]
        public int Month { get { return DateAdded.Month; } }
        [BsonIgnore]
        public int Day { get { return DateAdded.Day; } }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
