using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce1.Controllers
{
    public class MenuController : BaseController
    {
        private readonly IMenuApp _ImenuApp;

        public MenuController(IMenuApp imenuApp, IWebHostEnvironment webHostEnvironment, ILogger<ProdutoController> logger, 
            UserManager<ApplicationUser> userManager, ILogSistemaApp ilogsistemaApp) : base(logger, userManager, ilogsistemaApp)
        {
            _ImenuApp = imenuApp;
        }

        [AllowAnonymous]
        [HttpGet("/api/ListarMenu")]
        public async Task<IActionResult> ListarMenu()
        {
            var listaMenu = new List<Menu>();

            var usuario = await RetornarIdUsuarioLogado();

            listaMenu = await _ImenuApp.MontarMenuPorPerfil(usuario);

            return Json(new { listaMenu });

        }
    }
}
