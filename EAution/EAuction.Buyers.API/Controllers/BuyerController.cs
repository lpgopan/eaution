using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using MediatR;
using System.Net;
using System.Threading.Tasks;
using EAuction.BusinessApplication.DTOs;
using EAuction.BusinessApplication.DTOs.Buyer;
using EAuction.BusinessApplication.Features.Buyer.Requests.Command;

namespace EAuction.Buyers.API.Controllers
{
    [Route("/e-auction/api/v1/buyer")]
    [ApiController]
    public class BuyerController : Controller
    {
        private readonly IMediator _mediator;

        public BuyerController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [Route("place-bid")]
        [HttpPost]
        [ProducesResponseType(typeof(EAuction.Domain.Buyer), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<ListProductBidDto>>> BidProduct([FromBody] BuyerProductBidDto productBidDto)
        {
            var leaveAllocations = await _mediator.Send(new BidProductCommand() { ProductBidDto = productBidDto });
            return Ok(leaveAllocations);
        }

        [HttpGet("update-bid/{productId}/{buyerEmailld}/{newBidAmount}")]
        public async Task<ActionResult<ListProductBidDto>> Get(int productId, string buyerEmailld, int newBidAmount)
        {
            var leaveAllocation = await _mediator.Send(new UpdateBidAmountCommand
            {
                ProductId = productId,
                EmailId = buyerEmailld,
                BidAmount = newBidAmount
            });
            return Ok(leaveAllocation);
        }
    }
}
