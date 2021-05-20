using APIConsume.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace APIConsume.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {

            IEnumerable<userViewModal> user = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:52811/api/user");
                //HTTP GET
                var responseTask = client.GetAsync("user");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<userViewModal>>();
                    readTask.Wait();
                    user = readTask.Result;
                }
                else //web api sent error response 
                {                    
                    user = Enumerable.Empty<userViewModal>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(user);

         }

        public ActionResult create()
        {
            return View();
        }

        [HttpPost]       
        public ActionResult create(userViewModal user)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:52811/api/user");

                //HTTP POST
                var post = client.PostAsJsonAsync<userViewModal>("user", user);
                post.Wait();

                var result = post.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(user);
        }

        public ActionResult Edit(int id)
        {
            userViewModal user = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:52811/api/");

                var responseTask = client.GetAsync("user/" + id.ToString());
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<userViewModal>();
                    readTask.Wait();
                    user = readTask.Result;
                }
                //HTTP POST
               
            }
           
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(userViewModal user)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:52811/api/user");

                //HTTP POST
                var put = client.PutAsJsonAsync<userViewModal>("user", user);
                put.Wait();

                var result = put.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(user);
                }
                
            }

           
        }


        
        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:52811/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("user/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }





    }






}
