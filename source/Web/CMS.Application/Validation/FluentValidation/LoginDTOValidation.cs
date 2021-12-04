﻿using CMS.Application.Models.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Application.Validation.FluentValidation
{
   public class LoginDTOValidation:AbstractValidator<LoginDTO>
    {
        public LoginDTOValidation()
        {
            RuleFor(x => x.UserName).Empty().MaximumLength(20).WithMessage("Enter a username");
            RuleFor(x => x.Password).Empty().MaximumLength(20).WithMessage("Enter a password");
        }
    }
}
