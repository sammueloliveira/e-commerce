using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebSite_Apis.Model;
using WebSite_Apis.Token;

namespace WebSiteApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthController(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpPost("/api/Login")]
        public async Task<ActionResult> Login(LoginUser login)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, true);
            if (result.Succeeded)
            {
                var token = new TokenJWTBuilder()
                 .AddSecurityKey(JwtSecurityKey.Create("Secret_Key-12345678"))
             .AddSubject("Empresa E-Commerce")
             .AddIssuer("Teste.Securiry.Bearer")
             .AddAudience("Teste.Securiry.Bearer")
             .AddClaim("UsuarioAPINumero", "1")
             .AddExpiry(5)
             .Builder();

                return Ok(token.value);
            }

            return BadRequest("Error");
        }
    }
}
