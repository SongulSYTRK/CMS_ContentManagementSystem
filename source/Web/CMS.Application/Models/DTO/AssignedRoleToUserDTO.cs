using CMS.Domain.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Application.Models.DTO
{
   public  class AssignedRoleToUserDTO
    {

        public IdentityRole Role { get; set; }
        public IEnumerable<AppUser> HasRole { get; set; }
        public IEnumerable<AppUser> HasNoRole { get; set; }
        public string RoleName { get; set; }

        //Kullanıcıya role atarken birden fazla kullanıcıyı bir role atarken yada o rolden alırken kullanıcıların ıd'lerini taşımak için bu iki array kullanacağım
        public string[] AddIds { get; set; }
        public string[] DeleteIds { get; set; }
    }
}
