using CMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Domain.Entities.Interface
{
   public interface IBaseEntity
    {
        //Normally we defined abstract class but We will take base from more class. So we defined interfade to abstractclass
        public DateTime CreateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public Status Status { get; set; }
    }
}
