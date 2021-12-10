using CMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Helpers;

namespace CMS.Application.Models.DTO
{
    public class CreatePageDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
       // public string Slug => Title.ToLower().Replace(" ", "_");
        public string Slug => Crypto.GenerateSalt().ToLower()+Title;  //Crypto=> I do not want the client to see the url address


        public DateTime CreateDate => DateTime.Now;
        public Status Status => Status.Active;
    }
}
