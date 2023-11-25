using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces.InferfaceServices;
using Domain.Interfaces.InterfaceProduto;

namespace Application.OpenApp
{
    public class ProdutoApp : IProdutoApp
    {
        private readonly IProduto _Iproduto;
        private readonly IProdutoService _IserviceProduto;

        public ProdutoApp(IProduto produto, 
            IProdutoService serviceProduto)
        {
            _Iproduto = produto;
            _IserviceProduto = serviceProduto;
        }

        public async Task AddProduto(Produto produto)
        {
            await _IserviceProduto.AddProduto(produto);
        }

        public async Task UpdateProduto(Produto produto)
        {
            await _IserviceProduto.UpdateProduto(produto);
        }
        public async Task Add(Produto objeto)
        {
            await _Iproduto.Add(objeto);
        }
        public async Task Delete(Produto objeto)
        {
            await _Iproduto.Delete(objeto);
        }

        public async Task<Produto> GetEntityById(int Id)
        {

            return await _Iproduto.GetEntityById(Id);
        }

        public async Task<List<Produto>> List()
        {
            return await _Iproduto.List();
        }

        public async Task Update(Produto objeto)
        {
            await _Iproduto.Update(objeto);
        }

        public async Task<List<Produto>> ListarUsuarioLogado(string userId)
        {
            return await _Iproduto.ListarUsuarioLogado(userId);
        }

        public async Task<List<Produto>> ListarProdutosComEstoque(string descricao)
        {
            return await _IserviceProduto.ListarProdutosComEstoque(descricao);
        }

        public async Task<List<Produto>> ListarProdutosCarrinhoUsuario(string userid)
        {
            return await _Iproduto.ListarProdutosCarrinhoUsuario(userid);
        }

        public async Task<Produto> ObterProdutoCarrinho(int idProdutoCarrinho)
        {
            return await _Iproduto.ObterProdutoCarrinho(idProdutoCarrinho);
        }
    }
}
