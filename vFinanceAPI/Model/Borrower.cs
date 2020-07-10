using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace vFinanceAPI.Model
{
    public class Borrower 
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("borrowerId")]
        public string Id { get; set; }

        [BsonElement("name")]
        [Required(ErrorMessage ="Name is required.")]
        public string Name { get; set; }

        [BsonElement("mobileNumber")]
        [Required(ErrorMessage ="Mobile Number is required.")]
        public string MobileNumber { get; set; }

        [BsonElement("address")]
        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }

        [BsonElement("gender")]
        public string Gender { get; set; }

        [BsonElement("age")]
        public int Age { get; set; }

        [BsonElement("businessName")]
        public string BusinessName { get; set; }

        [BsonElement("businessAddress")]
        public string BusinessAddress { get; set; }

        [BsonElement("referenceName")]
        public string ReferenceName { get; set; }

        [BsonElement("referenceContactNumber")]
        public string ReferenceContactNumber { get; set; }      

        [BsonIgnore]        
        public List<Loan> Loans { get; set; }
      
    }
}
