using CMS.Application.Models.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Application.Validation.FluentValidation
{
   public class UpdateProfileDTOValidation:AbstractValidator<UpdateProfileDTO>
    {
        public UpdateProfileDTOValidation()
        {
            
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Enter a Fullname").MaximumLength(25).MinimumLength(3).WithMessage("Min character: 3, max character: 25");

            RuleFor(x => x.UserName).NotEmpty().WithMessage("Enter a username").MaximumLength(25).MinimumLength(3).WithMessage("Min character: 3, max character: 25");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Enter a mail").EmailAddress().WithMessage("Error email format");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Enter a password");

            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Enter a confirmpassword").Equal(x => x.Password).WithMessage("password dont match");
            RuleFor(x => x.ImagePath).NotEmpty().WithMessage("Choose a picture");
        }
    }
}
