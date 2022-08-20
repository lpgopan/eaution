using EAuction.BusinessApplication.Exceptions;
using EAuction.BusinessApplication.Features.Buyer.Requests.Command;
using EAuction.BusinessApplication.Persistance;
using EAuction.BusinessApplication.Response;
using EAuction.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EAuction.BusinessApplication.Features.Buyer.Handlers.Command
{
    public class UpdateBidAmountCommandHandler : IRequestHandler<UpdateBidAmountCommand, BaseCommandResponse>
    {
        private readonly IBuyerRepository _buyerRepository;
        private readonly IProductRepository _productRepository;
        private readonly ILogger _logger;
        public UpdateBidAmountCommandHandler(IBuyerRepository buyerRepository, IProductRepository productRepository
            , ILogger<UpdateBidAmountCommandHandler> logger)
        {
            this._buyerRepository = buyerRepository;
            this._productRepository = productRepository;
            _logger = logger;
        }
        public async Task<BaseCommandResponse> Handle(UpdateBidAmountCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"UpdateBidAmount Started at {DateTime.Now}");
            var response = new BaseCommandResponse();
            Domain.Buyer buyerDetails = await _buyerRepository.getBuyerDetailsByEmailId(request.EmailId);
            if (buyerDetails == null)
            {
                _logger.LogError("Unable to find buyer details by bid id");
                throw new BadRequestException("Unable to find buyer details by bid id");
            }
            try
            {
                Product product = await _productRepository.getProductByProductId(buyerDetails.ProductId);
                if (product.BidEndDate < DateTime.Now)
                {
                    _logger.LogError("Bid end date should be always future date");
                    throw new BadRequestException("Bid end date should be always future date");
                }
                buyerDetails.BidAmount = request.BidAmount;
                await _buyerRepository.UpdateBidAmout(buyerDetails.Id, buyerDetails);
            }
            catch (Exception ex)
            {
                String error = String.Format("BidAmount update failed. %s", ex.Message);
                _logger.LogError(error);
                throw new BadRequestException(error);
            }
            _logger.LogInformation($"Update bidamount completed at {DateTime.Now}");
            return response;
        }
    }
}
