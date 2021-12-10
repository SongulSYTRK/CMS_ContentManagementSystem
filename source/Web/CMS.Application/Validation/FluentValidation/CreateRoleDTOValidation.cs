using CMS.Application.Models.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Application.Validation.FluentValidation
{
   public  class CreateRoleDTOValidation:AbstractValidator<CreateRoleDTO>
    {
        public CreateRoleDTOValidation()
        {
            RuleFor(x => x.RoleName).NotEmpty().WithMessage("Enter a slug").MinimumLength(2).WithMessage("min character :2");
        }
    }
}
