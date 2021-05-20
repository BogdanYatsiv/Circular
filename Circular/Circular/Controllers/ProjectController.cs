using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using Circular.Models.JsonModels;
using Circular.Models.ViewModels;

namespace Circular.Controllers
{
    public class ProjectController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Project(string GithubLink)
        {
            //TO DO: заносити проект в базу даних
            string JsonRequestResult;
            ProjectResponse projectResponse = new ProjectResponse();
            //TO DO: правильно відформатувати посилання
            //var toFormat = GithubLink.Split("/");
            string apiUrl = string.Format("https://api.github.com/repos/{0}/{1}",
                "BogdanYatsiv", "Circular");

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

            //TO DO: спарсити як 1 об'єкт а не список

            //JsonRequestResult is an array of json nums
            //JArray jR = JArray.Parse(JsonRequestResult);

            //foreach (JObject value in jR)
            //{
            //    projectResponse = JsonConvert.DeserializeObject<ProjectResponse>(value.ToString());
            //}
            JObject jO = JObject.Parse(JsonRequestResult);
            projectResponse = JsonConvert.DeserializeObject<ProjectResponse>(jO.ToString());

            return View(projectResponse);
        }

        [HttpGet]
        public IActionResult CreateProject()
        {
            //return RedirectToAction("Project", "Project", new { GithubLink });
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject(CreateProjectModel model)
        {
            if(model.GithubLink.Length >= 0)
            {
                return RedirectToAction("Project", "Project", new { model.GithubLink });
            }

            return View();
        }
    }
}
