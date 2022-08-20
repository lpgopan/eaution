using System;
using System.Collections.Generic;
using System.Text;
using EAuction.BusinessApplication.DTOs;
using EAuction.BusinessApplication.Response;
using MediatR;
using EAuction.BusinessApplication.Features.Seller.Requests.Commands;
using EAuction.BusinessApplication.Persistance;
using EAuction.BusinessApplication.DTOs.Seller.Validators;
    using AutoMapper;
using EAuction.Domain;
using System.Threading.Tasks;
using System.Threading;
using MongoDB.Driver;
using EAuction.BusinessApplication.Exceptions;
using Microsoft.Extensions.Options;
using EAuction.BusinessApplication.Configuration;
using Microsoft.Extensions.Logging;

namespace EAuction.BusinessApplication.Features.Seller.Handlers.Commands
{
    class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, BaseCommandResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger _logger;

        public CreateProductCommandHandler(
           IProductRepository productRepository, ILogger<CreateProductCommandHandler> logger)
        {
            this._productRepository = productRepository;
            _logger = logger;
        }



        public async Task<BaseCommandResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CreateProduct Started at {DateTime.Now}");
            var response = new BaseCommandResponse();
            var validator = new CreateProductDtoValidator(_productRepository);
            var validationResult = await validator.ValidateAsync(request.ProductDto);
            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Product creation failed.";
                throw new ValidationException(validationResult);
                //  response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                if (request.ProductDto.BidEndDate < DateTime.Now)
                {
                    _logger.LogError("Bid end date should be always future date");
                    throw new BadRequestException("Bid end date should be always future date");
                }
                EAuction.Domain.Seller seller = new EAuction.Domain.Seller
                {
                    FirstName = request.ProductDto.FirstName,
                    LastName = request.ProductDto.LastName,
                    Address = request.ProductDto.Address,
                    City = request.ProductDto.City,
                    State = request.ProductDto.State,
                    Pin = request.ProductDto.Pin,
                    Phone = request.ProductDto.Phone,
                    Email = request.ProductDto.Email

                };
                Product product = new Product
                {
                    ProductId = await _productRepository.getMaxProductId(),
                    ProductName = request.ProductDto.ProductName,
                    ShortDescription = request.ProductDto.ShortDescription,
                    DetailedDescription = request.ProductDto.DetailedDescription,
                    Category = request.ProductDto.Category.ToString(),
                    Startingprice = request.ProductDto.Startingprice,
                    BidEndDate = request.ProductDto.BidEndDate,
                    DateCreated = DateTime.Now,
                    LastModifiedDate = DateTime.Now,
                    SellerInfo = seller
                };
                Task<Product> p = _productRepository.AddProduct(product);
                response.Message = "Product creation Successful";
            }
            _logger.LogInformation($"CreateProduct Ended at {DateTime.Now}");
            return response;
        }
    }
}
