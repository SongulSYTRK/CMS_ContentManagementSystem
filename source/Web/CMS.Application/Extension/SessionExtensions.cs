using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace CMS.Application.Extension
{
   public static  class SessionExtensions
    {
        //sepete gönderirken json tipine gönüştürüyorum
        public static void SetJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }


        //sepetten alırken json tipinden T tipine dönüştürüoyrum 
        public static T GetJson<T>(this ISession session, string key)
        {
            var sessionData = session.GetString(key);
            return sessionData == null ? default(T) : JsonConvert.DeserializeObject<T>(sessionData); 
        }

    }
}
