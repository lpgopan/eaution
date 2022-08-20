using EAuction.BusinessApplication.Configuration;
using EAuction.BusinessApplication.DTOs;
using EAuction.BusinessApplication.Response;
using EAuction.Domain;
using MediatR;
using MongoDB.Driver;

namespace EAuction.BusinessApplication.Features.Seller.Requests.Commands
{
    public class CreateProductCommand : IRequest<BaseCommandResponse>
    {
        public CreateProductDto ProductDto { get; set; }
        //public IMongoCollection<Product> MongoDbProduct { get; set; }
      //  public EAuctionConfiguration1 MongoDbProduct { get; set; }
    }
}
