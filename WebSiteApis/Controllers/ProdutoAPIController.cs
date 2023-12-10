using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebSiteApis.Controllers
{
    [Authorize]
    public class ProdutoAPIController : Controller
    {
        public readonly IProdutoApp _produtoApp;
        public readonly ICompraUsuarioApp _compraUsuarioApp;

        public ProdutoAPIController(IProdutoApp productApp, ICompraUsuarioApp compraUsuarioApp)
        {
            _produtoApp = productApp;
            _compraUsuarioApp = compraUsuarioApp;
        }


        [HttpGet("lista-produtos")]
        public async Task<JsonResult> ListaProdutos(string descricao)
        {
            return Json(await _produtoApp.ListarProdutosComEstoque(descricao));
        }
    }
}
