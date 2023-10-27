using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerce1.Controllers
{
    [Authorize]
    [LogActionFilter]
    public class UsuarioController : BaseController
    {
        private readonly IUsuarioApp _IusuarioApp;

        public UsuarioController(IUsuarioApp usuarioApp, IWebHostEnvironment webHostEnvironment,
            ILogger<ProdutoController> logger, UserManager<ApplicationUser> userManager,
            ILogSistemaApp ilogsistemaApp) : base(logger, userManager, ilogsistemaApp)
        {
            _IusuarioApp = usuarioApp;
        }

        // GET
        public async Task<IActionResult> ListarUsuarios()
        {
            if (!await UsuarioAdministrador())
                return RedirectToAction("Index", "Home");

            return View(await _IusuarioApp.ListarUsuarioSomenteParaAdministradores(await RetornarIdUsuarioLogado()));
        }
        // GET
        public async Task<IActionResult> Edit(string id)
        {
            if (!await UsuarioAdministrador())
                return RedirectToAction("Index", "Home");

            var tipoUsuarios = new List<SelectListItem>();

            tipoUsuarios.Add(new SelectListItem { Text = Enum.GetName(typeof(TipoUsuario), TipoUsuario.Comum), Value = Convert.ToInt32(TipoUsuario.Comum).ToString() });
            tipoUsuarios.Add(new SelectListItem { Text = Enum.GetName(typeof(TipoUsuario), TipoUsuario.Administrador), Value = Convert.ToInt32(TipoUsuario.Administrador).ToString() });
            ViewBag.TipoUsuarios = tipoUsuarios;

            return View(await _IusuarioApp.ObterUsuarioPeloID(id));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ApplicationUser usuario)
        {
            try
            {
                if (!await UsuarioAdministrador())
                    return RedirectToAction("Index", "Home");

                await _IusuarioApp.AtualizarTipoUsuario(usuario.Id, (TipoUsuario)usuario.Tipo);

                await LogEcommerce(TipoLog.Informativo, usuario);

                return RedirectToAction(nameof(ListarUsuarios));
            }
            catch (Exception erro)
            {
                await LogEcommerce(TipoLog.Erro, erro);

                return View("Edit", usuario);
            }
        }
    }
    
}

