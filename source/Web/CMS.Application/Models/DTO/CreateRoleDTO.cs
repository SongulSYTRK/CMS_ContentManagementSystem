using CMS.Domain.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Application.Models.DTO
{
   public  class CreateRoleDTO
    {

        public string  RoleName { get; set; }

        public List<AppUser> Users { get; set; }
    }
}
