using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace vFinanceAPI.Model
{
    public class LoanCollection
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("collectionId")]
        public string Id { get; set; }

        [BsonElement("collectionType")]       
        public string CollectionType { get; set; }

        [BsonElement("installmentAmount")]        
        public decimal InstallmentAmount { get; set; }

        [BsonElement("installmentDate")]      
        public DateTime InstallmentDate { get; set; }

        [BsonElement("paymentAmount")]
        [Required(ErrorMessage = "Payment Amount is required.")]
        public decimal PaymentAmount { get; set; }

        [BsonElement("paymnentDate")]
        public DateTime PaymnentDate { get; set; }

        [BsonElement("collectedBy")]
        public string CollectedBy { get; set; }

        [BsonElement("collectedAt")]
        public DateTime CollectedAt { get; set; }

        [BsonElement("status")]
        public string Status { get; set; }
     
    }
}
