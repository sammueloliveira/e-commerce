using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces.InferfaceServices;
using Domain.Interfaces.InterfaceCompraUsuario;

namespace Application.OpenApp
{
    public class CompraUsuarioApp : ICompraUsuarioApp
    {
        private readonly ICompraUsuario _IcompraUsuario;
        private readonly ICompraUsuarioService _IserviceCompraUsuario;

        public CompraUsuarioApp(ICompraUsuario compraUsuario, ICompraUsuarioService serviceCompraUsuario)
        {
            _IcompraUsuario = compraUsuario;
            _IserviceCompraUsuario = serviceCompraUsuario;
        }
        public async Task Add(CompraUsuario objeto)
        {
            await _IcompraUsuario.Add(objeto);
        }

        public async Task Delete(CompraUsuario objeto)
        {
            await _IcompraUsuario.Delete(objeto);
        }

        public async Task<CompraUsuario> GetEntityById(int Id)
        {
            return await _IcompraUsuario.GetEntityById(Id);
        }

        public async Task<List<CompraUsuario>> List()
        {
            return await _IcompraUsuario.List();
        }
        public async Task Update(CompraUsuario objeto)
        {
            await _IcompraUsuario.Update(objeto);
        }

        public async Task<int> QuantidadeProdutoCarrinhoUsuario(string userId)
        {
            return await _IcompraUsuario.QuantidadeProdutoCarrinhoUsuario(userId);
        }

        public async Task<bool> ConfirmaCompraCarrinhoUsuario(string userId)
        {
            return await _IcompraUsuario.ConfirmaCompraCarrinhoUsuario(userId);
        }

        public async Task<CompraUsuario> CarrinhoCompras(string userId)
        {
            return await _IserviceCompraUsuario.CarrinhoCompras(userId);
        }

        public async Task<CompraUsuario> ProdutosComprados(string userId, int? idCompra = null)
        {
            return await _IserviceCompraUsuario.ProdutosComprados(userId, idCompra);
        }

        public async Task<List<CompraUsuario>> MinhasCompras(string userId)
        {
            return await _IserviceCompraUsuario.MinhasCompras(userId);
        }

        public async Task AdicionarProdutoCarrinho(string userId, CompraUsuario compraUsuario)
        {
             await _IserviceCompraUsuario.AdicionarProdutoCarrinho(userId, compraUsuario);
        }
    }
}
