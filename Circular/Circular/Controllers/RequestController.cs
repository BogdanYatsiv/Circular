using Circular.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Circular.Models;
using Circular.Models.JsonModels;
using Newtonsoft.Json;

namespace Circular.Controllers
{
    public class RequestController : Controller
    {
        // GET: RequestController
        [HttpGet]
        public async Task<IActionResult> Commits(string ApiUrl = "https://api.github.com/repos/BogdanYatsiv/Circular/commits")
        {
            string JsonRequestResult;
            List<CommitResponse> commits = new List<CommitResponse>();

            //Request body
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ApiUrl);
            //UserAgent required for GitHub API
            request.UserAgent = "Google Chrome 40 (Win 8.1 x64): Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/40.0.2214.115 Safari/537.36";
            WebResponse response = await request.GetResponseAsync();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    JsonRequestResult = reader.ReadToEnd();
                }
            }
            response.Close();

            //JsonRequestResult is a array of json nums
            //TO DO: розпарсити джейсон і передати на сторінку нормальні дані
            JArray jR = JArray.Parse(JsonRequestResult);
            
            foreach (JObject value in jR)
            {
                CommitResponse commitResponse = JsonConvert.DeserializeObject<CommitResponse>(value.ToString());
                commits.Add(commitResponse);
            }

            ViewBag.Commits = commits;
            return View();
        }
    }
}
