using Domain.Entities;

namespace Domain.Interfaces.InferfaceServices
{
    public interface IServiceProduto
    {
        Task AddProduto(Produto produto);
        Task UpdateProduto(Produto produto);
        Task<List<Produto>> ListarProdutosComEstoque(string descricao);
    }
}
