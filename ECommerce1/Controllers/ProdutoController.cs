using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security;
using System.Security.Permissions;


namespace ECommerce1.Controllers
{
    [Authorize]
    [LogActionFilter]
    public class ProdutoController : BaseController
    {
        private readonly IProdutoApp _produtoApp;
        private readonly ICompraUsuarioApp _compraUsuarioApp;
        private IWebHostEnvironment _webHostEnvironment;

        public ProdutoController(IProdutoApp produtoApp, ICompraUsuarioApp IcompraUsuarioApp,
            IWebHostEnvironment webHostEnvironment, ILogger<ProdutoController> logger, UserManager<ApplicationUser> userManager,
            ILogSistemaApp ilogsistemaApp) : base(logger, userManager, ilogsistemaApp)
        {
            _produtoApp = produtoApp;
            _compraUsuarioApp = IcompraUsuarioApp;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var idUsuario = await RetornarIdUsuarioLogado();

            return View(await _produtoApp.ListarUsuarioLogado(idUsuario));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            return View(await _produtoApp.GetEntityById(id));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Produto produto)
        {
            try
            {
                var idUsuario = await RetornarIdUsuarioLogado();
                produto.UserId = idUsuario;

                await _produtoApp.AddProduto(produto);
                await SalvarImagemProduto(produto);
                await LogEcommerce(TipoLog.Informativo, produto);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception erro)
            {
                await LogEcommerce(TipoLog.Erro, erro);

                return View();
            }
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _produtoApp.GetEntityById(id));
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Produto produto)
        {
            try
            {
                await _produtoApp.UpdateProduto(produto);
               
                return RedirectToAction(nameof(Index));
            }
            catch
            {
               return View();
            }
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            return View(await _produtoApp.GetEntityById(id));
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Produto produto)
        {
            try
            {
                var deletarProduto = await _produtoApp.GetEntityById(id);
                await _produtoApp.Delete(deletarProduto);

                await LogEcommerce(TipoLog.Informativo, deletarProduto);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception erro)
            {
                await LogEcommerce(TipoLog.Erro, erro);

                return View();
            }
        }

      
        [AllowAnonymous]
        [HttpGet("/api/ListarProdutosComEstoque")]
        public async Task<JsonResult> ListarProdutosComEstoque(string descricao)
        {
            return Json(await _produtoApp.ListarProdutosComEstoque(descricao));
        }

       
        [HttpGet("/api/listar-produtos-carrinho-usuario")]
        public async Task<IActionResult> ListarProdutosCarrinhoUsuario()
        {
            var idUsuario = await RetornarIdUsuarioLogado();

            return View(await _produtoApp.ListarProdutosCarrinhoUsuario(idUsuario));
        }

        public async Task<IActionResult> RemoverCarrinho(int id)
        {
            return View(await _produtoApp.ObterProdutoCarrinho(id));
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoverCarrinho(int id, Produto produto)
        {
            try
            {

                var deletarProduto = await _compraUsuarioApp.GetEntityById(id);
                await _compraUsuarioApp.Delete(deletarProduto);

                return RedirectToAction(nameof(ListarProdutosCarrinhoUsuario));
            }
            catch (Exception erro)
            {
                await LogEcommerce(TipoLog.Erro, erro);

                return View();
            }
        }

        public async Task SalvarImagemProduto(Produto produtoTela)
        {
            try
            {
                var produto = await _produtoApp.GetEntityById(produtoTela.Id);

                if (produtoTela.Imagem != null)
                {
                    var webRoot = _webHostEnvironment.WebRootPath;
                    var permissionSet = new PermissionSet(PermissionState.Unrestricted);
                    var writePermission = new FileIOPermission(FileIOPermissionAccess.Append, string.Concat(webRoot, "/imgProdutos"));
                    permissionSet.AddPermission(writePermission);

                    var Extension = System.IO.Path.GetExtension(produtoTela.Imagem.FileName);

                    var NomeArquivo = string.Concat(produto.Id.ToString(), Extension);

                    var diretorioArquivoSalvar = string.Concat(webRoot, "\\imgProdutos\\", NomeArquivo);

                    produtoTela.Imagem.CopyTo(new FileStream(diretorioArquivoSalvar, FileMode.Create));

                    produto.Url = string.Concat("https://localhost:7005", "/imgProdutos/", NomeArquivo);

                    await _produtoApp.UpdateProduto(produto);
                }
            }
            catch (Exception erro)
            {
                await LogEcommerce(TipoLog.Erro, erro);
            }

        }


    }
}
