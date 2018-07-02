namespace MeTube.App.Controllers
{
    using Services.DataProvider;
    using Services.DataProvider.Contracts;
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Controllers;
    using SimpleMvc.Framework.Interfaces;
    using System.Text;

    public class UserController : Controller
    {
        private readonly IDataService service;

        public UserController()
        {
            this.service = new DataService();
        }


        [HttpGet]
        public IActionResult Profile()
        {


            AuthorizeUser();

            var user = this.service.GetUserByName(this.User.Name);

            this.Model["name"] = user.Username;
            this.Model["mail"] = user.Email;

            var tubesInfo = new StringBuilder();

            foreach (var tube in user.Tubes)
            {
                tubesInfo.AppendLine("<tr>" +
                    $"<td>{tube.Id}</td>" +
                    $"<td>{tube.Title}</td>" +
                    $"<td>{tube.Author}</td>" +
                    $"<td><a href= \"/tube/details?id={tube.Id}\">Details</td>" +
                    "</tr>");
            }

            this.Model["tubeInfo"] = tubesInfo.ToString();
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
