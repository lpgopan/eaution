using EAuction.BusinessApplication.DTOs.Buyer;
using EAuction.BusinessApplication.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EAuction.BusinessApplication.Features.Buyer.Requests.Command
{
    public class BidProductCommand : IRequest<BaseCommandResponse>
    {
        public BuyerProductBidDto ProductBidDto { get; set; }
    }
}
