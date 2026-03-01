using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Model;
using SchoolManagement.Services;
using System.Security.Claims;

namespace SchoolManagement.Controllers
{
    public class AuthuserController : Controller
    {
        private readonly ApiUserService _apiUserService;

        public AuthuserController()
        {
            _apiUserService = new ApiUserService();
        }
        public IActionResult Login()
        {
            LoginRequestDTO UserloginRequest = new LoginRequestDTO();
            return View(UserloginRequest);
        }

        [HttpPost("LoginUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginUser(LoginRequestDTO userLoginRequest)
        {
            LoginResponceDTO UserloginRequest = new LoginResponceDTO();
            UserloginRequest = await _apiUserService.AuthenticationUser(userLoginRequest);
            if (UserloginRequest != null && UserloginRequest.Token.ToString() != "")
            {
                var  identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Name, UserloginRequest.UserDetails.Name));
                identity.AddClaim(new Claim(ClaimTypes.Role, UserloginRequest.UserDetails.Role));
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                HttpContext.Session.SetString("APIToken", UserloginRequest.Token);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                HttpContext.Session.SetString("APIToken", "");
                return RedirectToAction("Login", "Authuser");

            }
            return View(UserloginRequest);
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.SetString("APIToken", "");
            return RedirectToAction("Login", "Authuser");
        }

        public IActionResult AccessDenied()
        {
            LoginRequestDTO UserloginRequest = new LoginRequestDTO();
            return View(UserloginRequest);
        }
    }
}
