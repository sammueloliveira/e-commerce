using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces
{
     public interface ICompraUsuarioApp : IGenericApp<CompraUsuario>
    {
        public Task<int> QuantidadeProdutoCarrinhoUsuario(string userId);
        public Task<bool> ConfirmaCompraCarrinhoUsuario(string userId);
        public Task<CompraUsuario> CarrinhoCompras(string userId);
        public Task<CompraUsuario> ProdutosComprados(string userId, int? idCompra = null);
        public Task<List<CompraUsuario>> MinhasCompras(string userId);
        public Task AdicionarProdutoCarrinho(string userId, CompraUsuario compraUsuario);

    }


}

