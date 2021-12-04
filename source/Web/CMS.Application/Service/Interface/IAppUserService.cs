using CMS.Application.Models.DTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Service.Interface
{
    public interface IAppUserService
    {
        Task<IdentityResult> Register(RegisterDTO model);
        Task<SignInResult> Login(LoginDTO model);

        Task LogOut();
        Task UpdateUser(UpdateProfileDTO model);

        Task<UpdateProfileDTO> GetById(int id);
        Task<int> UserIdFormName(string userName);
    }
}
