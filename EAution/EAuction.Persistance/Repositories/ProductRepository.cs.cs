
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EAuction.BusinessApplication.Persistance;
using EAuction.Domain;
using EAuction.BusinessApplication.Configuration;
using Microsoft.Extensions.Options;

namespace EAuction.Persistance.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private IMongoCollection<Product> _product;
        public ProductRepository()
        {

        }
        public ProductRepository(IOptions<EAuctionConfiguration> settings)
        {
            // _settings = settings.Value;
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _product = database.GetCollection<Product>(settings.Value.SellerCollectionName);
        }
        public async Task<Product> AddProduct(Product product)
        {
            try
            {
                await _product.InsertOneAsync(product);
                return product;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }


        Task<Product> IProductRepository.UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
        public async Task UpdateProduct(Product product)
        {
             await _product.ReplaceOneAsync(c => c.Id.Equals(product.Id), product);
        }

        public async Task<Product> getProductById(int id)
        {
            return await _product.Find<Product>(c => c.Id.Equals(id)).FirstOrDefaultAsync();
        }
        public async Task<Product> getProductByProductId(int id)
        {
            return await _product.Find<Product>(c => c.ProductId.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<Product> getProductByName(string productName)
        {
            return await _product.Find<Product>(c => c.ProductName.Equals(productName)).FirstOrDefaultAsync();
        }

        public async Task<List<Product>> getProducts()
        {
            List<Product> producst = await _product.Find(c => true).ToListAsync();
            return producst;
        }
        public async Task<int> getMaxProductId()
        {
            Product maxProduct = await _product.Find(c => true).SortByDescending(d => d.ProductId).Limit(1).FirstOrDefaultAsync();
            return maxProduct != null ? maxProduct.ProductId + 1: 1;
        }

        public async Task DeleteProductByProductId(int id)
        {
            await _product.DeleteOneAsync(c => c.ProductId.Equals(id));
        }
        public async Task DeleteProductByProductName(string productName)
        {
            await _product.DeleteOneAsync(c => c.ProductName.Equals(productName));
        }

        public async Task DeleteProduct(string id)
        {
            await _product.DeleteOneAsync(c => c.Id.Equals(id));
        }

    }
}
