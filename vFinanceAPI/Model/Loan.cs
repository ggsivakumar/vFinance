using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace vFinanceAPI.Model
{
    public class Loan
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("loanId")]
        public string Id { get; set; }

        [BsonElement("loanCategory")]
        [Required(ErrorMessage = "Loan Category is required.")]
        public string LoanCategory { get; set; }

        [BsonElement("loanType")]
        [Required(ErrorMessage = "Loan Type is required.")]
        public string LoanType { get; set; }

        [BsonElement("loanPrincipalAmount")]
        [Required(ErrorMessage = "Loan Principal Amount is required.")]
        public decimal LoanPrincipalAmount { get; set; }

        [BsonElement("documentCharges")]
        public decimal DocumentCharges { get; set; }

        [BsonElement("interestRate")]
        public decimal InterestRate { get; set; }

        [BsonElement("interestAmount")]
        public string InterestAmount { get; set; }

        [BsonElement("noOfInstallments")]
        public string Installments { get; set; }

        [BsonElement("loanPerInstallment")]
        public string LoanPerInstallment { get; set; }

        [BsonElement("startDate")]
        public DateTime StartDate { get; set; }

        [BsonElement("endDate")]
        public DateTime EndDate { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> LoanCollections { get; set; }

        [BsonIgnore]
        [BsonElement("loanCollections")]
        public List<LoanCollection> LoanCollectionList { get; set; }
    }
}
