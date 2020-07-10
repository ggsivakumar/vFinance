using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vFinanceAPI.Model;

namespace vFinanceAPI.Services
{
    public class TransactionService
    {
        private readonly IMongoCollection<Transaction> _transaction;

        public TransactionService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _transaction = database.GetCollection<Transaction>(settings.TransactionCollectionName);
        }


        public async Task<List<Transaction>> GetAllAsync()
        {
            return await _transaction.Find(b => true).ToListAsync();
        }

        public async Task<Transaction> GetByIdAsync(string id)
        {
            return await _transaction.Find<Transaction>(s => s.Id == id).FirstOrDefaultAsync();
        }
      
        public async Task<List<Transaction>> GetByDateAsync(DateTime fromDate, DateTime toDate)
        {
            var loanCollection = await _transaction.Find<Transaction>(s => s.Date >= fromDate && s.Date <= toDate).ToListAsync();           
            return loanCollection;

        }

        public async Task<Transaction> CreateAsync(Transaction transaction)
        {
            await _transaction.InsertOneAsync(transaction);
            return transaction;
        }

        public async Task UpdateAsync(string id, Transaction transaction)
        {
            await _transaction.ReplaceOneAsync(s => s.Id == id, transaction);
        }

        public async Task DeleteAsync(string id)
        {
            await _transaction.DeleteOneAsync(s => s.Id == id);
        } 

    }
}
