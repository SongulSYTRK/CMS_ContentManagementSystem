using CMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Application.Models.DTO
{
    public class UpdatePageDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Slug => Title.ToLower().Replace(" ", "_");


        
        public DateTime UpdateDate => DateTime.Now;
        public Status Status => Status.Modified;
    }
}
