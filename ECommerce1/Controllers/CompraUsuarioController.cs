using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using ECommerce1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce1.Controllers
{

    public class CompraUsuarioController : HelpQrCode
    {
        private readonly ICompraUsuarioApp _compraUsuarioApp;
        private readonly UserManager<ApplicationUser> _userManager;
        private IWebHostEnvironment _webHostEnvironment;

        public CompraUsuarioController(ICompraUsuarioApp compraUsuarioApp, UserManager<ApplicationUser> userManager,
             IWebHostEnvironment webHostEnvironment)
        {
            _compraUsuarioApp = compraUsuarioApp;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> FinalizarCompra()
        {
            var usuario = await _userManager.GetUserAsync(User);
            var compraUsuario = await _compraUsuarioApp.CarrinhoCompras(usuario.Id);

            return View(compraUsuario);
        }

        public async Task<IActionResult> MinhasCompras(bool mensagem = false)
        {
            var usuario = await _userManager.GetUserAsync(User);
            var compraUsuario = await _compraUsuarioApp.MinhasCompras(usuario.Id);

            if(mensagem)
            {
                ViewBag.Sucesso = true;
                ViewBag.Mensagem = "Compra efetivada com sucesso. Pague o boleto para garantir sua compra!";
            }

            return View(compraUsuario);
        }

        public async Task<IActionResult> ConfirmaCompras()
        {
            var usuario = await _userManager.GetUserAsync(User);

            var sucesso = await _compraUsuarioApp.ConfirmaCompraCarrinhoUsuario(usuario.Id);

            if(sucesso)
            {
                return RedirectToAction("MinhasCompras", new { mensagem = true });
            }
            else
            {
                return RedirectToAction("FinalizarCompra");
            }
        }

        [HttpPost("/api/adicionar-produto-carrinho")]
        public async Task<IActionResult> AdicionarProdutoCarrinho(string id, string nome, string qtd)
        {
            var usuario = await _userManager.GetUserAsync(User);

            if (usuario != null)
            {
                await _compraUsuarioApp.AdicionarProdutoCarrinho(usuario.Id, new CompraUsuario
                {
                    IdProduto = Convert.ToInt32(id),
                    QtdCompra = Convert.ToInt32(qtd),
                    Estado = EstadoCompra.Produto_Carrinho,
                    UserId = usuario.Id
                });
                return Json(new { sucesso = true });
            }

            return Json(new { sucesso = false });

        }
        [HttpGet("/api/qtd-produto-carrinho")]
        public async Task<JsonResult> QtdProdutoCarrinho()
        {
            var usuario = await _userManager.GetUserAsync(User);

            var qtd = 0;

            if(usuario != null)
            {
                qtd = await _compraUsuarioApp.QuantidadeProdutoCarrinhoUsuario(usuario.Id);

                return Json(new { sucesso = true, qtd = qtd });
            }

            return Json(new { seucesso = false, qtd = qtd });
        }

        public async Task<IActionResult> Imprimir(int id)
        {
            var usuario = await _userManager.GetUserAsync(User);

            var compraUsuario = await _compraUsuarioApp.ProdutosComprados(usuario.Id, id);

            return await Download(compraUsuario, _webHostEnvironment);

        }


    }
}   
