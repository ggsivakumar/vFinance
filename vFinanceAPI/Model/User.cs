using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace vFinanceAPI.Model
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]       
        public string Id { get; set; }

        [BsonElement("userName")]
        [Required(ErrorMessage = "UserName is required.")]
        public string UserName { get; set; }

        [BsonElement("userId")]
        [Required(ErrorMessage = "UserId is required.")]
        public string UserId { get; set; }
        
        [BsonElement("password")]
        public string Password { get; set; }
    }
}
