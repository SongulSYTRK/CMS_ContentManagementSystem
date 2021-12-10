using CMS.Domain.Entities.Interface;
using CMS.Domain.Enums;

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Domain.Entities.Concrete
{
    public class AppUserRole : IdentityRole, IBaseEntity
    {
        public DateTime CreateDate  { get; set; }
        public DateTime? DeleteDate  { get; set; }
        public DateTime? UpdateDate  { get; set; }
        public Status Status  { get; set; }
            }
}
