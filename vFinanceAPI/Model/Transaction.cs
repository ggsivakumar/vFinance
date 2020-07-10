using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vFinanceAPI.Model
{
    public class Transaction
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("transactionType")]
        public string TransactionType { get; set; }

        [BsonElement("category")]
        public string Category { get; set; }

        [BsonElement("amount")]
        public double Amount { get; set; }

        [BsonElement("date")]
        public DateTime Date { get; set; }
    }
}
