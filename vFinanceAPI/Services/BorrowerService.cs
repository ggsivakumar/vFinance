using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vFinanceAPI.Model;

namespace vFinanceAPI.Services
{
    public class BorrowerService
    {
        private readonly IMongoCollection<Borrower> _borrower;

        public BorrowerService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _borrower = database.GetCollection<Borrower>(settings.BorrowersCollectionName);
        }

        public async Task<List<Borrower>> GetAllAsync()
        {
            return await _borrower.Find(b => true).ToListAsync();
        }

        public async Task<Borrower> GetByIdAsync(string id)
        {
            return await _borrower.Find<Borrower>(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Borrower> CreateAsync(Borrower borrower)
        {
            await _borrower.InsertOneAsync(borrower);
            return borrower;
        }

        public async Task UpdateAsync(string id, Borrower borrower)
        {
            await _borrower.ReplaceOneAsync(s => s.Id == id, borrower);
        }

        public async Task DeleteAsync(string id)
        {
            await _borrower.DeleteOneAsync(s => s.Id == id);
        }
    }
}
