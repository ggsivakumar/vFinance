using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vFinanceAPI.Model
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string BorrowersCollectionName { get; set; }
        public string LoansCollectionName { get; set; }
        public string CollectionsCollectionName { get; set; }
        public string DocumentCollectionName { get; set; }
        public string UserCollectionName { get; set; }
        public string TransactionCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IDatabaseSettings
    {
        string BorrowersCollectionName { get; set; }        
        string LoansCollectionName { get; set; }
        string CollectionsCollectionName { get; set; }
        string DocumentCollectionName { get; set; }
        string UserCollectionName { get; set; }
        string TransactionCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
