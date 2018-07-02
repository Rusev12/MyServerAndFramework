namespace MeTube.App.Controllers
{
    using Services.DataProvider;
    using Services.DataProvider.Contracts;
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Controllers;
    using SimpleMvc.Framework.Interfaces;
    using System.Text;

    public class HomeController : Controller
    {
        private readonly IDataService service;

        public HomeController()
        {
            this.service = new DataService();
        }

        [HttpGet]
        public IActionResult Index()
        {
            AuthorizeUser();

            if (!this.User.IsAuthenticated)
            {
                return View();
            }

            return RedirectToAction("/home/alltubes");

        }

        [HttpGet]
        public IActionResult Alltubes()
        {
            AuthorizeUser();

            var username = this.User.Name;

            var tubes = this.service.GetAllTubes();

            this.Model["name"] = username;

            var tubesInfo = new StringBuilder();

            tubesInfo.AppendLine("<div class=\"row\">");
            foreach (var tube in tubes)
            {
                tubesInfo.AppendLine("<div class=\"col-md-4\">");
                tubesInfo.AppendLine($"<iframe src = \"https://www.youtube.com/embed/{tube.YoutubeId}\" ></iframe>");
                tubesInfo.AppendLine($"<p>{tube.Title}</p>");
                tubesInfo.AppendLine($"<p>{tube.Author}</p>");
                tubesInfo.AppendLine("</div>");

            }


            tubesInfo.AppendLine("</div>");

            this.Model["songs"] = tubesInfo.ToString();

            return View();
        }

        private void AuthorizeUser()
        {
            if (this.User.IsAuthenticated)
            {
                this.Model["displayType"] = "block";

                this.Model["notLoginUser"] = "none";
            }
            else
            {
                this.Model["notLoginUser"] = "block";

                this.Model["displayType"] = "none";
            }
        }
    }
}
