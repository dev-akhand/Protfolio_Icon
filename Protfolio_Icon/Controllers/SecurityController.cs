using Microsoft.AspNetCore.Mvc;
using Protfolio_Icon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Protfolio_Icon.Controllers
{
    public class SecurityController : Controller
    {
        private const string LOGIN_ROUTENAME = "apslogin";

        [HttpGet]
        [Route(LOGIN_ROUTENAME)]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [Route(LOGIN_ROUTENAME)]
        public async Task<IActionResult> Login(LoginModel model)
        {
            return View();
        }
    }
}
