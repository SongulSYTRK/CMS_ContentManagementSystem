using CMS.Application.Models.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Application.Validation.FluentValidation
{
    public class CreatePageDTOValidation:AbstractValidator<CreatePageDTO>
    {
        public CreatePageDTOValidation()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("enter a Title").MaximumLength(20).MinimumLength(3).WithMessage("character min:3, max:20");
            RuleFor(x => x.Content).NotEmpty().WithMessage("enter a Content").MaximumLength(50).MinimumLength(3).WithMessage("character min:3, max:50");
            
        }
    }
}
