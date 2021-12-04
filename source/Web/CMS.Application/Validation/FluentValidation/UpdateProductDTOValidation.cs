using CMS.Application.Models.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Application.Validation.FluentValidation
{
    public class UpdateProductDTOValidation:AbstractValidator<UpdateProductDTO>
    {
        public UpdateProductDTOValidation()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(20).WithMessage("Enter a CategoryName");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Enter a Description").MaximumLength(200).MinimumLength(5).WithMessage("character min : 5 max : 200");
            RuleFor(x => x.ImagePath).NotEmpty().MaximumLength(200).WithMessage("Enter a Imagepath");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Enter a Price");
        }
    }
}
