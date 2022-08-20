using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using EAuction.Domain;
using EAuction.BusinessApplication.Persistance;
using EAuction.BusinessApplication.Configuration;
using Microsoft.Extensions.Options;

namespace EAuction.Persistance.Repositories
{
    public class BuyerRepository : IBuyerRepository
    {
        private readonly IMongoCollection<Buyer> _buyer;
        public BuyerRepository(IOptions<EAuctionConfiguration> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _buyer = database.GetCollection<Buyer>(settings.Value.BuyerCollectionName);
        } 
        public async Task<Buyer> BidProduct(Buyer buyer)
        {
            await _buyer.InsertOneAsync(buyer);
            return buyer;
        }

        public async Task<Buyer> UpdateBidAmout(string id, Buyer buyer)
        {
            await _buyer.ReplaceOneAsync(c => c.Id.Equals(id), buyer);
            return buyer;
        }

        public async Task<int> getMaxBidId()
        {
            Buyer maxBuyer = await _buyer.Find(c => true).SortByDescending(d => d.BidId).Limit(1).FirstOrDefaultAsync();
            return maxBuyer != null ? maxBuyer.BidId : 0;
        }

        public async Task<Buyer> getBuyerDetailsByEmailId(string emailId)
        {
            return await _buyer.Find(c => c.Email.Equals(emailId)).FirstOrDefaultAsync();
        }
        public async Task<List<Buyer>> getBuyerDetailsByProductId(int id)
        {
            return await _buyer.Find(c => c.ProductId.Equals(id)).ToListAsync();
        }
        public async Task<Buyer> getBuyerDetailsByBidId(int id)
        {
            return await _buyer.Find(c => c.BidId.Equals(id)).FirstOrDefaultAsync();
        }

    }
}
