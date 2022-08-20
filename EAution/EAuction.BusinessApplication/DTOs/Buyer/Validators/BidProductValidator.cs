using EAuction.BusinessApplication.Persistance;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace EAuction.BusinessApplication.DTOs.Buyer.Validators
{
    public class BidProductValidator : AbstractValidator<BuyerProductBidDto>
    {
        private readonly IBuyerRepository _buyerRepository;
        public BidProductValidator(IBuyerRepository buyerRepository)
        {
            _buyerRepository = buyerRepository;

            RuleFor(x => x.FirstName).NotNull()
                .WithMessage("{PropertyName} must not be null.")
                .Must(x => x.Length >= 5 && x.Length <= 30)
                .WithMessage("{PropertyName} must have minimum of 5 and maximum of 30 characters.");

            RuleFor(x => x.LastName).NotNull()
                .WithMessage("{PropertyName} must not be null.")
                .Must(x => x.Length >= 5 && x.Length <= 25)
                .WithMessage("{PropertyName} must have minimum of 5 and maximum of 30 characters.");

            RuleFor(x => x.Email).NotNull()
                .WithMessage("{PropertyName} must not be null.")
               .EmailAddress().WithMessage("A valid email is required");

            RuleFor(x => x.Phone).NotNull()
                .WithMessage("{PropertyName} must not be null.")
                .Must(x => x >= 0)
                .WithMessage("{PropertyName} must be all numeric.")
                .Must(x => x.ToString().Length == 10)
                .WithMessage("{PropertyName} must have minimum of 10 and maximum of 10 characters.");
        }
    }
}
