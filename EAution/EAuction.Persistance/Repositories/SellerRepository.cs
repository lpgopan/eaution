using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using EAuction.Domain;
using EAuction.BusinessApplication.Persistance;

namespace EAuction.Persistance.Repositories
{
    public class SellerRepository : ISellerRepository
    {
        private readonly IMongoCollection<Seller> _seller;
        public SellerRepository(IMongoCollection<Seller> mongoDbProduct)
        {
            _seller = mongoDbProduct;
        }
        public async Task<Seller> AddSellerInformation(Seller seller)
        {
            await _seller.InsertOneAsync(seller);
            return seller;
        }
    }
}
