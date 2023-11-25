using Domain.Entities;

namespace Domain.Interfaces.InferfaceServices
{
    public interface ICompraUsuarioService
    {
        public Task<CompraUsuario> CarrinhoCompras(string userId);
        public Task<CompraUsuario> ProdutosComprados(string userId, int? idCompra = null);
        public Task<List<CompraUsuario>> MinhasCompras(string userId);
        public Task AdicionarProdutoCarrinho(string userId, CompraUsuario compraUsuario);
    }
}
