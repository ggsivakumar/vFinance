using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vFinanceAPI.Model;

namespace vFinanceAPI.Services
{
    public class DocumentService
    {
        private readonly IMongoCollection<Document> _document;

        public DocumentService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _document = database.GetCollection<Document>(settings.DocumentCollectionName);
        }

        public async Task<Document> GetByIdAsync(string id)
        {
            return await _document.Find<Document>(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Document> CreateAsync(Document document)
        {
            await _document.InsertOneAsync(document);
            return document;
        }

        public async Task DeleteAsync(string id)
        {
            await _document.DeleteOneAsync(s => s.Id == id);
        }
    }
}
