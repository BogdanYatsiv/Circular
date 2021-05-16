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
        [HttpGet]
        public async Task<IActionResult> Commits(string RepoUrl = "https://github.com/BogdanYatsiv/Circular")
        {
            string JsonRequestResult;
            List<CommitResponse> commits = new List<CommitResponse>();
            var toFormat = RepoUrl.Split("/");
            string apiUrl = string.Format("https://api.github.com/repos/{0}/{1}/commits",
                toFormat[toFormat.Count() - 2], toFormat[toFormat.Count() - 1]);

            //Request body
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
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

            //JsonRequestResult is an array of json nums
            JArray jR = JArray.Parse(JsonRequestResult);
            
            foreach (JObject value in jR)
            {
                CommitResponse commitResponse = JsonConvert.DeserializeObject<CommitResponse>(value.ToString());
                commits.Add(commitResponse);
            }

            return View(commits);
        }
    }
}
