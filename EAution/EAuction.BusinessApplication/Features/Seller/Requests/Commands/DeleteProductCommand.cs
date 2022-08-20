using EAuction.BusinessApplication.DTOs.Seller;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EAuction.BusinessApplication.Features.Seller.Requests.Commands
{
    public class DeleteProductCommand : IRequest
    {
        public int productId { get; set; } 
    }
}
