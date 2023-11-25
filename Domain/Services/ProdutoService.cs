using Domain.Entities;
using Domain.Interfaces.InferfaceServices;
using Domain.Interfaces.InterfaceProduto;

namespace Domain.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProduto _Iproduto;

        public ProdutoService(IProduto produto)
        {
            _Iproduto = produto;
        }
        public async Task AddProduto(Produto produto)
        {
            produto.DataCadastro = DateTime.Now;
            produto.DataAlteracao = DateTime.Now;
            produto.Estado = true;

            await _Iproduto.Add(produto);
        }

        public async Task UpdateProduto(Produto produto)
        {
            produto.DataAlteracao = DateTime.Now;

            await _Iproduto.Update(produto);
        }

        public async Task<List<Produto>> ListarProdutosComEstoque(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao))
                return await _Iproduto.ListarProdutos(p => p.QtdEstoque > 0);
            else
            {
                return await _Iproduto.ListarProdutos(p => p.QtdEstoque > 0
                && p.Nome.ToUpper().Contains(descricao.ToUpper()));
            }
        }


    }
}