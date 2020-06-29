using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vFinanceAPI.Model
{
    public class Document
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]        
        public string Id { get; set; }

      
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("borrowerId")]
        public string BorrowerId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("loanId")]
        public string LoanId { get; set; }
        
        [BsonElement("document")]
        public byte[] File { get; set; }
    }
}
