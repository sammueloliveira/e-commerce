using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce1.Controllers
{
    [Authorize]
    [LogActionFilter]
    public class LogSistemaController : BaseController
    {
        private readonly ILogSistemaApp _logSistemaApp;

        public LogSistemaController(ILogSistemaApp logSistemaApp, IWebHostEnvironment webHostEnvironment,
            ILogger<ProdutoController> logger, UserManager<ApplicationUser> userManager,
            ILogSistemaApp ilogsistemaApp) : base(logger, userManager, ilogsistemaApp)
        {
            _logSistemaApp = logSistemaApp;
        }

      
        public async Task<IActionResult> Index()
        {
            if(!await UsuarioAdministrador())
                return RedirectToAction("Index", "Home");

            return View(await _logSistemaApp.List());
        }

       
        public async Task<IActionResult> Details(int? id)
        {

            if (!await UsuarioAdministrador())
                return RedirectToAction("Index", "Home");

            if (id == null )
            {
                return NotFound();
            }

            var logSistema = await _logSistemaApp.GetEntityById((int)id);
            if (logSistema == null)
            {
                return NotFound();
            }

            return View(logSistema);
        }

       
    }
}
