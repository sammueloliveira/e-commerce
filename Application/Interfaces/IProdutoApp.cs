using Domain.Entities;

namespace Application.Interfaces
{
    public interface IProdutoApp : IGenericApp<Produto>
    {
        Task AddProduto(Produto produto);
        Task UpdateProduto(Produto produto);
        Task<List<Produto>> ListarUsuarioLogado(string userId);
        Task<List<Produto>> ListarProdutosComEstoque(string descricao);
        Task<List<Produto>> ListarProdutosCarrinhoUsuario(string userid);
        Task<Produto> ObterProdutoCarrinho(int idProdutoCarrinho);
    }
}
