namespace MeTube.App.Controllers
{
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Controllers;
    using SimpleMvc.Framework.Interfaces;
    using Models.BindingModels;
    using Services.DataProvider.Contracts;
    using Services.DataProvider;
    using MeTube.Models;
    using SimpleMvc.Common;

    public class AccountController : Controller
    {
        private readonly IDataService service;

        public AccountController()
        {
            this.service = new DataService();
        }


        [HttpGet]
        public IActionResult Register()
        {
            this.Model["error"] = string.Empty;
            AutorizeUser();

            return View();
        }

      
        [HttpPost]
        public IActionResult Register(RegisterBindingModel model)
        {

            if (model.Password != model.ConfirmPassword)
            {
                this.Model["error"] = "Passwords doesn't match!";
                this.AutorizeUser();
                return View();
            }

            var hashPassword = PasswordHash.ComputeSha256Hash(model.Password);

            var user = new User()
            {
                Username = model.Username,
                Email = model.Email,
                PasswordHash = hashPassword,

            };

            this.service.RegisterUser(user);

            this.SignIn(model.Username);
    

            return RedirectToAction("/");
        }

        [HttpGet]
        public IActionResult Login()
        {
            this.Model["error"] = string.Empty;
            AutorizeUser();

            return View();
        }



        [HttpPost]
        public IActionResult Login(LoginBindingModel model)
        {

            var isUserExists = this.service.IsUserExist(model.Username);
            var user = this.service.GetUserByName(model.Username);
            var passwordHash = PasswordHash.ComputeSha256Hash(model.Password);



            if (user == null || user.PasswordHash != passwordHash)
            {
                this.Model["error"] = "username or password is wrong!";
                this.AutorizeUser();
                return View();
            }

            if (isUserExists)
            {
                this.SignIn(model.Username);
                this.InitializeUser();
            }

            
            return RedirectToAction("/");
        }


        [HttpGet]
        public IActionResult Logout()
        {
            this.SignOut();
            return RedirectToAction("/");
        }


        private void AutorizeUser()
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
