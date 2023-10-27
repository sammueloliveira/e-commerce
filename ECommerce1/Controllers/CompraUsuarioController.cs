using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using ECommerce1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce1.Controllers
{
   
    public class CompraUsuarioController : HelpQrCode
    {
        private readonly ICompraUsuarioApp _IcompraUsuarioApp;
        private readonly UserManager<ApplicationUser> _userManager;
        private IWebHostEnvironment _webHostEnvironment;

        public CompraUsuarioController(ICompraUsuarioApp compraUsuarioApp, UserManager<ApplicationUser> userManager,
             IWebHostEnvironment webHostEnvironment)
        {
            _IcompraUsuarioApp = compraUsuarioApp;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> FinalizarCompra()
        {
            var usuario = await _userManager.GetUserAsync(User);
            var compraUsuario = await _IcompraUsuarioApp.CarrinhoCompras(usuario.Id);

            return View(compraUsuario);
        }

        public async Task<IActionResult> MinhasCompras(bool mensagem = false)
        {
            var usuario = await _userManager.GetUserAsync(User);
            var compraUsuario = await _IcompraUsuarioApp.MinhasCompras(usuario.Id);

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

            var sucesso = await _IcompraUsuarioApp.ConfirmaCompraCarrinhoUsuario(usuario.Id);

            if(sucesso)
            {
                return RedirectToAction("MinhasCompras", new { mensagem = true });
            }
            else
            {
                return RedirectToAction("FinalizarCompra");
            }
        }

        [HttpPost("/api/AdicionarProdutoCarrinho")]
        public async Task<IActionResult> AdicionarProdutoCarrinho(string id, string nome, string qtd)
        {
            var usuario = await _userManager.GetUserAsync(User);

            if (usuario != null)
            {
                await _IcompraUsuarioApp.AdicionarProdutoCarrinho(usuario.Id, new CompraUsuario
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
        [HttpGet("/api/QtdProdutoCarrinho")]
        public async Task<JsonResult> QtdProdutoCarrinho()
        {
            var usuario = await _userManager.GetUserAsync(User);

            var qtd = 0;

            if(usuario != null)
            {
                qtd = await _IcompraUsuarioApp.QuantidadeProdutoCarrinhoUsuario(usuario.Id);

                return Json(new { sucesso = true, qtd = qtd });
            }

            return Json(new { seucesso = false, qtd = qtd });
        }

        public async Task<IActionResult> Imprimir(int id)
        {
            var usuario = await _userManager.GetUserAsync(User);

            var compraUsuario = await _IcompraUsuarioApp.ProdutosComprados(usuario.Id, id);

            return await Download(compraUsuario, _webHostEnvironment);

        }


    }
}   
