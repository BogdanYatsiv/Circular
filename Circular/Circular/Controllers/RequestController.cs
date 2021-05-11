using Circular.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Circular.Controllers
{
    public class RequestController : Controller
    {
        // GET: RequestController
        [HttpGet]
        public async Task<IActionResult> Commits(string ApiUrl = "https://api.github.com/repos/BogdanYatsiv/Circular/commits")
        {
            string RequestResult;
            WebRequest request = WebRequest.Create(ApiUrl);
            WebResponse response = await request.GetResponseAsync();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    RequestResult = reader.ReadToEnd();
                }
            }
            response.Close();
            ViewData["RequestResult"] = RequestResult;
            return View();
        }
    }
}
