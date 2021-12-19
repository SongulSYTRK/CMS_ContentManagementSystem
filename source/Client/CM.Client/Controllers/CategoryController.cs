
using CM.Client.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CM.Client.Controllers
{
    
        public class CategoryController : Controller
        {
            public async Task<IActionResult> List()
            {

                List<Category> categories = new List<Category>();

                using (var httpClient = new HttpClient())
                {
                    //Url database ile konuşuyor.//API sayfasında ; Get kısmında Request Url'den geliyor.
                    using var request = await httpClient.GetAsync("http://localhost:48542/api/Category");


                    //string olarak seriliaze ediyor.. 
                    string response = await request.Content.ReadAsStringAsync();

                    // Microsoft.AspNetCore.Mvc.Newtonsoftjson - 3.1.22 paketini jsonconvert için indirdik.

                    //json olan bir veriyi  category dönüştürmek için deserialize kullanılır.

                    categories = JsonConvert.DeserializeObject<List<Category>>(response);
                }

                return View(categories);
            }

            public IActionResult Create()
            {
                return View();
            }

            //viewden controllere geldi onu json çevriip request atıyorum ve post ediyorum.

            [HttpPost]
            public async Task<IActionResult> Create(Category category)
            {
                if (ModelState.IsValid)
                {
                    using (var htppClient = new HttpClient())
                    {
                        //json çevirmek için seriliaze ediyoruz. 
                        //Encoding.UTF8 => Türkçe karakter desteği 
                        var content = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");
                        //content ekliyoruz içerik için.//request atıyorum
                        using var request = await htppClient.PostAsync("http://localhost:48542/api/Category", content);
                        //eklenmiş datayı görmek için list git dedik.//başarılı olunca
                        return RedirectToAction("List");
                    }
                }
                //başarısız olunca view dön
                return View();
            }

            public async Task<IActionResult> Update(int id)
            {
                Category category = new Category();

                using (var httpClient = new HttpClient())
                {
                    using var request = await httpClient.GetAsync($"http://localhost:48542/api/Category/{id}"); //idsinden yakalıyoruz.
                    string response = await request.Content.ReadAsStringAsync();
                    //json olan bir veriyi category dönüştürmek için deserialize kullanılır.
                    category = JsonConvert.DeserializeObject<Category>(response);
                }
                return View(category);
            }

            [HttpPost]
            public async Task<IActionResult> Update(Category category)
            {
                if (ModelState.IsValid)
                {
                    using (var httpClient = new HttpClient())
                    {
                        //json çevirmek için seriliaze ediyoruz. content sor ??
                        var content = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");
                        using var request = await httpClient.PutAsync("http://localhost:48542/api/Category", content);

                    }
                    return RedirectToAction("List");
                }
                return View();
            }

            public async Task<IActionResult> Delete(int id)
            {

                using (var httpClient = new HttpClient())
                {
                    using var request = await httpClient.DeleteAsync($"http://localhost:48542/api/Category/{id}");
                }

                return RedirectToAction("List");
            }


        }
}


