using EAuction.BusinessApplication.DTOs.Seller;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EAuction.BusinessApplication.Features.Seller.Requests.Queries
{
    public class GetProductsQueries : IRequest<List<ProductListDto>>
    {
    }
}
