using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace DTO
{
    public class GuestBookEntry : MongoEntityBase
    {
        public string Name { get; set; }
        public string Message { get; set; }
        public DateTime DateAdded { get; set; }

    }
}
