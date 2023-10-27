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
        private readonly ILogSistemaApp _IlogSistemaApp;

        public LogSistemaController(ILogSistemaApp IlogSistemaApp, IWebHostEnvironment webHostEnvironment,
            ILogger<ProdutoController> logger, UserManager<ApplicationUser> userManager,
            ILogSistemaApp ilogsistemaApp) : base(logger, userManager, ilogsistemaApp)
        {
            _IlogSistemaApp = IlogSistemaApp;
        }

        // GET: LogSistema
        public async Task<IActionResult> Index()
        {
            if(!await UsuarioAdministrador())
                return RedirectToAction("Index", "Home");

            return View(await _IlogSistemaApp.List());
        }

        // GET: LogSistema/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (!await UsuarioAdministrador())
                return RedirectToAction("Index", "Home");

            if (id == null )
            {
                return NotFound();
            }

            var logSistema = await _IlogSistemaApp.GetEntityById((int)id);
            if (logSistema == null)
            {
                return NotFound();
            }

            return View(logSistema);
        }

       
    }
}
