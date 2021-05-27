using AsistLabProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace AsistLabProject.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        [HttpGet]
        public async Task<List<Posts>> GetPosts()
        {
            var client = new RestClient("https://jsonplaceholder.typicode.com/posts");
            var request = new RestRequest(Method.GET);
            IRestResponse response = await client.ExecuteAsync(request);
            var content = JsonConvert.DeserializeObject<JArray>(response.Content);
            var posts = content.Select(x => new Posts
            {
                Id = (int)x["id"],
                UserId = (int)x["userId"],
                Title = (string)x["title"],
                Body = (string)x["body"]
            }).ToList();

            return posts;
        }

        [HttpPost]
        public IActionResult Posts([FromBody] List<Posts> posts)
        {
            return View(posts);
        }
    }
}
