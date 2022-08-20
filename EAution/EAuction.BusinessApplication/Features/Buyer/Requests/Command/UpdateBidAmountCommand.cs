using EAuction.BusinessApplication.DTOs.Buyer;
using EAuction.BusinessApplication.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EAuction.BusinessApplication.Features.Buyer.Requests.Command
{
    public class UpdateBidAmountCommand : IRequest<BaseCommandResponse>
    {
        public UpdateBidAmountDto ProductBidDto { get; set; }

        public int  ProductId { get; set; }
        public string EmailId { get; set; }
        public int BidAmount { get; set; }
    }
}
