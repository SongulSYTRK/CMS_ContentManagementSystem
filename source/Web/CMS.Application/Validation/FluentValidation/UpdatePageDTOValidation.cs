using CMS.Application.Models.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Application.Validation.FluentValidation
{
    public class UpdatePageDTOValidation:AbstractValidator<UpdatePageDTO>
    {
        public UpdatePageDTOValidation()
        {
            RuleFor(x => x.Title).MaximumLength(20).NotEmpty().WithMessage("enter a Title");
            RuleFor(x => x.Content).MaximumLength(20).NotEmpty().WithMessage("enter a Content");
            RuleFor(x => x.Slug).NotEmpty().WithMessage("enter a Slug");
        }
    }
}
