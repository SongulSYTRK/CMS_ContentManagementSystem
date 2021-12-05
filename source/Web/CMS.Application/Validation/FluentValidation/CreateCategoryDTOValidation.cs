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
            RuleFor(x => x.Name).NotEmpty().WithMessage("Enter a CategoryName").MaximumLength(20).MinimumLength(3).WithMessage("character min:3 max:20");
            RuleFor(x => x.Slug).NotEmpty().WithMessage("Enter a slug");
        }
    }
}
