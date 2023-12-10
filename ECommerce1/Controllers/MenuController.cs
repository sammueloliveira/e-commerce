using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce1.Controllers
{
    public class MenuController : BaseController
    {
        private readonly IMenuApp _menuApp;

        public MenuController(IMenuApp menuApp, IWebHostEnvironment webHostEnvironment, ILogger<ProdutoController> logger, 
            UserManager<ApplicationUser> userManager, ILogSistemaApp ilogsistemaApp) : base(logger, userManager, ilogsistemaApp)
        {
            _menuApp = menuApp;
        }

        [AllowAnonymous]
        [HttpGet("/api/listar-menu")]
        public async Task<IActionResult> ListarMenu()
        {
            var listaMenu = new List<Menu>();

            var usuario = await RetornarIdUsuarioLogado();

            listaMenu = await _menuApp.MontarMenuPorPerfil(usuario);

            return Json(new { listaMenu });

        }
    }
}
