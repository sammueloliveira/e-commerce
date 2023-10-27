using Domain.Entities;
using Domain.Interfaces.InterfaceGeneric;
using System.Linq.Expressions;

namespace Domain.Interfaces.InterfaceProduto
{
    public interface IProduto : IGeneric<Produto>
    {
        Task<List<Produto>> ListarUsuarioLogado(string userId);
        Task<List<Produto>> ListarProdutos(Expression<Func<Produto, bool>> exProduto);
        Task<List<Produto>> ListarProdutosCarrinhoUsuario(string userid);
        Task<Produto> ObterProdutoCarrinho(int idProdutoCarrinho);
    }
}
