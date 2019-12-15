using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class CommentDetails
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("RestaurantID")] 
        public Guid RestaurantID { get; set; }

        [BsonElement("Location")] 
        public string LocationCity { get; set; }

        [BsonElement] 
        public string Subject { get; set; }

        [BsonElement] 
        public string ExtraInfo { get; set; }

        [BsonIgnoreIfNull] 
        public string[] Coordinates { get; set; }

        [BsonElement("DateOfCreation")] 
        public DateTime DateOfCreation { get; set; } = DateTime.Now;
    }
}
