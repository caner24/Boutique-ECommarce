using Boutique.Entities.Concrate;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boutique.Business.CrosCuttingConcerns.Validation.FluentValidation
{
    public class ProductDetailValidator:AbstractValidator<ProductDetail>
    {
        public ProductDetailValidator()
        {
            RuleFor(x => x.ProductDetailText).NotNull().WithMessage(" Ürün detayi boş geçilemez ");
            RuleFor(x => x.Amount).NotNull().WithMessage(" Ürünün fiyati boş geçilemez ");
            RuleFor(x => x.Color).NotNull().WithMessage(" Ürünün rengi boş geçilemez ");
        }
    }
}
