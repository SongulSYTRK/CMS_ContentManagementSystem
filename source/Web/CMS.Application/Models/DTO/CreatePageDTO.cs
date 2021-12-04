using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Application.Models.DTO
{
    public class CreatePageDTO
    {
    
        public string Title { get; set; }
        public string Content { get; set; }
        public string Slug { get; set; }
    }
}
