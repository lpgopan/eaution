using EAuction.Domain;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EAuction.BusinessApplication.Persistance
{
    public interface IProductRepository
    {
        //Task setCollectionName(IMongoCollection<Product> mongoCollection);
        Task<Product> AddProduct(Product product);
        Task<Product> UpdateProduct(Product product);
        Task DeleteProduct(string id);
        Task<Product> getProductById(int id);
        Task<Product> getProductByProductId(int id);
        Task<Product> getProductByName(string name);
        Task<List<Product>> getProducts();
        Task<int> getMaxProductId();
        Task DeleteProductByProductName(string productName);
        Task DeleteProductByProductId(int productId);
    }
}
