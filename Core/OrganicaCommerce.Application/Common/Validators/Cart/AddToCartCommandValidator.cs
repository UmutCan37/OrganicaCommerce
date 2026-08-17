using FluentValidation;
using OrganicaCommerce.Application.CQRS.Cart.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganicaCommerce.Application.Common.Validators.Cart
{
    public class AddToCartCommandValidator : AbstractValidator<AddToCartCommand>
    {
        public AddToCartCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("Kullanıcı bilgisi gereklidir.");

            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("Ürün bilgisi gereklidir.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Miktar sıfırdan büyük olmalıdır.");
        }
    }
}
