using CMS.Application.Models.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Application.Validation.FluentValidation
{
    public class CreateCategoryDTOValidation:AbstractValidator<CreateCategoryDTO>
    {
        public CreateCategoryDTOValidation()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(20).WithMessage("Enter a CategoryName");
            RuleFor(x => x.Slug).NotEmpty().WithMessage("Enter a slug");
        }
    }
}
