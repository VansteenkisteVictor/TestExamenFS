using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    public class Comment
    {

        //BsonId als string voorstellen 
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }  //must voor mapping ipv public Object Id { get; set; }

        [BsonElement("RestaurantID")]
        public Guid RestaurantID { get; set; }

        [Range(typeof(decimal), "0", "10")]
        [BsonElement("Quotation")]
        public decimal Quotation { get; set; }


        //Relationele data in CommentDetails object (navigatie property alike)
        [BsonElement]
        public IEnumerable<CommentDetails> Details { get; set; }


        [BsonElement("DateOfCreation")]
        public DateTime DateOfCreation { get; set; } = DateTime.Now;


    }
}
