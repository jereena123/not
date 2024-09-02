using Demomvc.Models;
using Demomvc.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Demomvc.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginRepository _loginRepository;

        public LoginController(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        
        [HttpGet]
        public IActionResult Index()
        {
            //passing the message to the view using ViewBag
            //Viewbag dynamic property that allows you
            //to pass data from the conteoller
            // ViewBag.Message = "Welcome";
            return View();
        }

        [HttpPost]

        public IActionResult Index([Bind] Login login)
        {
            try
            {
                //call repository  implememntation
                int result = _loginRepository.UserCredentials(login);

                //check condition
                if (result == 1)
                {
                    //assuming yo want to paas the users name to the Successpaghe
                    ViewBag.UserName = login.UserName;
                   return View("Sucesspage");
                    // return RedirectToAction("Index","Home");

                }
                else
                {
                    //ModelState.AddModelError("UserName","Invalid Username");
                    ModelState.AddModelError("Password", "Invalid Username/Pasword");
                }

                return View(login);
            }
            catch
            {
                return RedirectToAction("Index", "Login");
            }
            //passing the message to the view using ViewBag
            //Viewbag dynamic property that allows you
            //to pass data from the conteoller
            // ViewBag.Message = "Welcome";

        }
    }
}