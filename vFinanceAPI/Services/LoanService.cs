using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vFinanceAPI.Model;

namespace vFinanceAPI.Services
{
    public class LoanService
    {
        private readonly IMongoCollection<Loan> _loan;

        public LoanService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _loan = database.GetCollection<Loan>(settings.LoansCollectionName);
        }

        public async Task<List<Loan>> GetAllAsync()
        {
            return await _loan.Find(b => true).ToListAsync();
        }

        public async Task<Loan> GetByIdAsync(string id)
        {
            return await _loan.Find<Loan>(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Loan>> GetByBorrowerIdAsync(string borrowerId)
        {
            return await _loan.Find<Loan>(s => s.BorrowerId == borrowerId).ToListAsync();
        }

        public async Task<Loan> CreateAsync(Loan Loan)
        {
            await _loan.InsertOneAsync(Loan);
            return Loan;
        }

        public async Task UpdateAsync(string id, Loan Loan)
        {
            await _loan.ReplaceOneAsync(s => s.Id == id, Loan);
        }

        public async Task DeleteAsync(string id)
        {
            await _loan.DeleteOneAsync(s => s.Id == id);
        }
    }
}
