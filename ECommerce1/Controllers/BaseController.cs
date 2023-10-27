using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ECommerce1.Controllers
{
    public class BaseController : Controller
    {
        public readonly ILogger<BaseController> logger;
        public readonly UserManager<ApplicationUser> userManager;
        public readonly ILogSistemaApp IlogsistemaApp;

        public BaseController(ILogger<BaseController> logger, UserManager<ApplicationUser> userManager, ILogSistemaApp ilogsistemaApp)
        {
            this.logger = logger;
            this.userManager = userManager;
            this.IlogsistemaApp = ilogsistemaApp;
        }

        public async Task LogEcommerce(TipoLog tipoLog, Object objeto)
        {
            string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();

            await IlogsistemaApp.Add(new LogSistema
            {
                TipoLog = tipoLog,
                JsonInformacao = JsonConvert.SerializeObject(objeto),
                UserId = await RetornarIdUsuarioLogado(),
                NomeAction = actionName,
                NomeController = controllerName
            });
        }
        public async Task<string> RetornarIdUsuarioLogado()
        {
            if(userManager != null)
            {
                var idUsuario = await userManager.GetUserAsync(User);
                return idUsuario != null ? idUsuario.Id : string.Empty;
            }

            return string.Empty;
        }
        public async Task<bool> UsuarioAdministrador()
        {
            if (userManager != null)
            {
                var idUsuario = await userManager.GetUserAsync(User);
                if(idUsuario != null && idUsuario.Tipo != null)
                {
                    if ((TipoUsuario)idUsuario.Tipo == TipoUsuario.Administrador)
                        return true;
                }
            }

            return false;
        }

    }
}
