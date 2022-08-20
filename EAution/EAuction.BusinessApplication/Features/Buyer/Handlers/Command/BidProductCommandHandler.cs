using Confluent.Kafka;
using EAuction.BusinessApplication.Configuration;
using EAuction.BusinessApplication.DTOs.Buyer.Validators;
using EAuction.BusinessApplication.Exceptions;
using EAuction.BusinessApplication.Features.Buyer.Requests.Command;
using EAuction.BusinessApplication.Persistance;
using EAuction.BusinessApplication.Response;
using EAuction.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace EAuction.BusinessApplication.Features.Buyer.Handlers.Command
{
    public class BidProductCommandHandler : IRequestHandler<BidProductCommand, BaseCommandResponse>
    {
        private readonly IBuyerRepository _buyerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOptions<EAuctionConfiguration> _configurations;
        private readonly ILogger _logger;
        public BidProductCommandHandler(IBuyerRepository buyerRepository, IProductRepository productRepository, IOptions<EAuctionConfiguration> configurations)
        {
            this._buyerRepository = buyerRepository;
            this._productRepository = productRepository;
            this._configurations = configurations;
        }

        public async Task<BaseCommandResponse> Handle(BidProductCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new BidProductValidator(_buyerRepository);
            var validationResult = await validator.ValidateAsync(request.ProductBidDto);
            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Product Bid creation failed.";
                throw new ValidationException(validationResult);
            }
            else
            {
                Product product = await _productRepository.getProductByProductId(request.ProductBidDto.ProductId);
                if (product.BidEndDate < DateTime.Now)
                {
                    throw new BadRequestException("Bid end date should be always future date");
                }
                if (product.Buyers != null)
                {
                    foreach (int buyer in product.Buyers)
                    {
                        Domain.Buyer buyerDetails = await _buyerRepository.getBuyerDetailsByBidId(buyer);
                        if (buyerDetails != null && buyerDetails.Email.Equals(request.ProductBidDto.Email))
                        {
                            throw new BadRequestException("More than one bid on a product by same user (based on email ID) is not allowed");
                        }
                    }

                }

                Domain.Buyer bidProduct = new Domain.Buyer
                {
                    BidId = await _buyerRepository.getMaxBidId(),
                    ProductId = product.ProductId,
                    FirstName = request.ProductBidDto.FirstName,
                    LastName = request.ProductBidDto.LastName,
                    Address = request.ProductBidDto.Address,
                    City = request.ProductBidDto.City,
                    State = request.ProductBidDto.State,
                    Pin = request.ProductBidDto.Pin,
                    Phone = request.ProductBidDto.Phone,
                    Email = request.ProductBidDto.Email,
                    BidAmount = request.ProductBidDto.BidAmount
                };
                Task<Domain.Buyer> bidProductResponse = _buyerRepository.BidProduct(bidProduct);
                if (_configurations.Value.EnableKafka)
                {
                    await bidProductToKafka(product, bidProduct);
                }
                response.Success = true;
                response.Message = "Product bidding was successful";
            }

            return response;
        }

        private static async Task bidProductToKafka(Product product, Domain.Buyer bidProduct)
        {
            try
            {
                var config = new ProducerConfig
                {
                    BootstrapServers = "localhost:9092",
                    // SaslMechanism = SaslMechanism.Plain,
                    //   SecurityProtocol = SecurityProtocol.SaslSsl,
                };

                int prod = product.ProductId;
                using var p = new ProducerBuilder<int, string>(config).Build();
                Random rand = new Random();
                var message = new Message<int, string>
                {
                    Value = JsonSerializer.Serialize<Domain.Buyer>(bidProduct),
                    Key = rand.Next()
                };

                // Send the message to our test topic in Kafka                
                var dr = await p.ProduceAsync("product_bid", message);
            }
            catch (Exception ex)
            {
                throw new BadRequestException(String.Format("Failed to send message to producer {0} with error {1}",
                    "topic product_bid", ex.Message));
            }
        }
    }
}
