using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebSiteApis.Controllers
{
    [Authorize]
    public class ProdutoAPIController : Controller
    {
        public readonly IProdutoApp _IProdutoApp;
        public readonly ICompraUsuarioApp _IcompraUsuarioApp;

        public ProdutoAPIController(IProdutoApp ProductApp, ICompraUsuarioApp CompraUsuarioApp)
        {
            _IProdutoApp = ProductApp;
            _IcompraUsuarioApp = CompraUsuarioApp;
        }


        [HttpGet("/api/ListaProdutos")]
        public async Task<JsonResult> ListaProdutos(string descricao)
        {
            return Json(await _IProdutoApp.ListarProdutosComEstoque(descricao));
        }
    }
}
