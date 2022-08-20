using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EAuction.Domain;

namespace EAuction.BusinessApplication.Persistance
{
    public interface IBuyerRepository
    {
        Task<Buyer> BidProduct(Buyer buyer);
        Task<Buyer> UpdateBidAmout(string id, Buyer buyer);
        Task<Buyer> getBuyerDetailsByEmailId(string id);
        Task<List<Buyer>> getBuyerDetailsByProductId(int id);
        Task<Buyer> getBuyerDetailsByBidId(int id);
        Task<int> getMaxBidId();
    }
}
