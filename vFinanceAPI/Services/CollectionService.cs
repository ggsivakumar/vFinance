using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vFinanceAPI.Model;

namespace vFinanceAPI.Services
{
    public class CollectionService
    {
        private readonly IMongoCollection<LoanCollection> _collection;

        public CollectionService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<LoanCollection>(settings.CollectionsCollectionName);
        }

        public async Task<List<LoanCollection>> GetAllAsync()
        {
            return await _collection.Find(b => true).ToListAsync();
        }

        public async Task<LoanCollection> GetByIdAsync(string id)
        {
            return await _collection.Find<LoanCollection>(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<LoanCollection>> GetByDateAsync(DateTime fromDate,DateTime toDate,string status)
        {
            var loanCollection = await _collection.Find<LoanCollection>(s => s.InstallmentDate >= fromDate && s.InstallmentDate <= toDate).ToListAsync();
            loanCollection = loanCollection.FindAll(s => s.Status == status);
            return loanCollection;            
                    
        }

        public async Task<LoanCollection> CreateAsync(LoanCollection collation)
        {
            await _collection.InsertOneAsync(collation);
            return collation;
        }

        public async Task UpdateAsync(string id, LoanCollection loanCollection)
        {
            await _collection.ReplaceOneAsync(s => s.Id == id, loanCollection);
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(s => s.Id == id);
        }
    }

}

