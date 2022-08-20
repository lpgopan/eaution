using AutoMapper;
using EAuction.BusinessApplication.Configuration;
using EAuction.BusinessApplication.DTOs.Seller;
using EAuction.BusinessApplication.Features.Seller.Requests.Queries;
using EAuction.BusinessApplication.Persistance;
using EAuction.BusinessApplication.Response;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EAuction.BusinessApplication.Features.Seller.Handlers.Queries
{
    public class GetProductsQueriesHandler : IRequestHandler<GetProductsQueries, List<ProductListDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger _logger;

        public GetProductsQueriesHandler(
           IProductRepository productRepository
            , ILogger<GetProductsQueriesHandler> logger
            )
        {
            this._productRepository = productRepository;
            _logger = logger;
        }

        public async Task<List<ProductListDto>> Handle(GetProductsQueries request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetProduct Started at {DateTime.Now}");
            var products = await _productRepository.getProducts();
            List<ProductListDto> productDtos = new List<ProductListDto>();
            foreach (var p in products)
            {
                productDtos.Add(new ProductListDto
                {
                    ProductName = p.ProductName,
                    ProductId = p.ProductId
                });
            }
            _logger.LogInformation($"Get product completed at {DateTime.Now}");
            return productDtos;
        }
    }
}
