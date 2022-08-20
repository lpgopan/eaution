
using EAuction.BusinessApplication.Configuration;
using EAuction.BusinessApplication.Exceptions;
using EAuction.BusinessApplication.Features.Seller.Requests.Commands;
using EAuction.BusinessApplication.Persistance;
using EAuction.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EAuction.BusinessApplication.Features.Seller.Handlers.Commands
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger _logger;

        public DeleteProductCommandHandler(
           IProductRepository productRepository
            , IOptions<EAuctionConfiguration> configurations
            , ILogger<DeleteProductCommandHandler> logger)
        {
            this._productRepository = productRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"DeleteProduct Started at {DateTime.Now}");
                var productDetails = await _productRepository.getProductByProductId(request.productId);

                if (productDetails == null)
                    throw new NotFoundException(nameof(Product), request.productId);
                if (productDetails.BidEndDate < DateTime.Now)
                {
                    _logger.LogError("Product cannot be deleted after Bid end date");
                    throw new BadRequestException("Product cannot be deleted after Bid end date");
                }
                else if (productDetails.Buyers != null)
                {
                    _logger.LogError("Product cannot be deleted as bid  was placed on the product");
                    throw new BadRequestException("Product cannot be deleted as bid  was placed on the product");
                }

                await _productRepository.DeleteProductByProductId(request.productId);

            }
            catch (Exception ex)
            {
                String error = String.Format("Product deletion failed. {0}", ex.Message);
                _logger.LogError(error);
                throw new BadRequestException(error);
            }
            _logger.LogInformation($"Delete product completed at {DateTime.Now}");
            return Unit.Value;
        }
    }
}
