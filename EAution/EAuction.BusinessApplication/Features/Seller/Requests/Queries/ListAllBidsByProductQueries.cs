using EAuction.BusinessApplication.DTOs.Seller;
using EAuction.BusinessApplication.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EAuction.BusinessApplication.Features.Seller.Requests.Queries
{
    public class ListAllBidsByProductQueries : IRequest<SellerBidListResponse>
    {
        public int ProductId { get; set; }
    }
}
