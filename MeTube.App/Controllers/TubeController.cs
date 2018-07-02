namespace MeTube.App.Controllers
{
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Controllers;
    using SimpleMvc.Framework.Interfaces;
    using MeTube.App.Models.BindingModels;
    using MeTube.Models;
    using Services.DataProvider.Contracts;
    using Services.DataProvider;

    public class TubeController : Controller
    {
        private readonly IDataService service;

        public TubeController()
        {
            this.service = new DataService();
        }


        [HttpGet]
        public IActionResult Upload()
        {
            AuthorizeUser();

            return View();
        }



        [HttpPost]
        public IActionResult Upload(TubeBindingModel model)
        {

            if (!this.IsValidModel(model))
            {
                return View();
            }
            var youtubeId = model.YoutubeLink.Split("?v=")[1];
            var username = this.User.Name;
            var user = this.service.GetUserByName(username);

            if (user == null)
            {
                return RedirectToAction("/account/login");
            }

            var tube = new Tube()
            {
                Title = model.Title,
                Author = model.Author,
                Description = model.Description,
                YoutubeId = youtubeId,
                User = user,
                UserId = user.Id
                
            };

            this.service.CreateTube(tube);
            
            return RedirectToAction("/");
        }
        
        [HttpGet]
        public IActionResult Details(int id)
        {
            AuthorizeUser();

            var tube = this.service.GetTubeById(id);

            this.service.IncreaseVies(tube);

            this.Model["youtubeId"] = tube.YoutubeId;

            this.Model["author"] = tube.Author;

            this.Model["views"] = tube.Views.ToString();

            this.Model["description"] = tube.Description;

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
