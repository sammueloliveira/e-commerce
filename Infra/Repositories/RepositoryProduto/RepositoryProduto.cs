using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.InterfaceProduto;
using Infra.Data;
using Infra.Repositories.RepositoryGeneric;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infra.Repositories.RepositoryProduto
{
    public class RepositoryProduto : RepositoryGeneric<Produto>, IProduto
    {
        private readonly DbContextOptions<Contexto> _dbContextOptions;

        public RepositoryProduto()
        {
            _dbContextOptions = new DbContextOptions<Contexto>();
        }

        public async Task<List<Produto>> ListarProdutos(Expression<Func<Produto, bool>> exProduto)
        {
          using (var banco = new Contexto(_dbContextOptions))
            {
                return await banco.Produto.Where(exProduto).AsNoTracking().ToListAsync();
            }
        }

        public async Task<List<Produto>> ListarUsuarioLogado(string userId)
        {
            using (var banco = new Contexto(_dbContextOptions))
            {
                return await banco.Produto.Where(p => p.UserId == userId).AsNoTracking().ToListAsync();
            }
        }

        public async Task<List<Produto>> ListarProdutosCarrinhoUsuario(string userid)
        {
            using (var banco = new Contexto(_dbContextOptions))
            {
                var produtosCarrinhoUsuario =  await (from p in banco.Produto
                                               join c in banco.CompraUsuario on p.Id equals c.IdProduto
                                               join co in banco.Compra on c.IdCompra equals co.Id
                                               where c.UserId.Equals(userid) && c.Estado == EstadoCompra.Produto_Carrinho
                                               select new Produto
                                               {
                                                   Id = p.Id,
                                                   Nome = p.Nome,
                                                   Descricao = p.Descricao,
                                                   Observacao = p.Observacao,
                                                   Valor = p.Valor,
                                                   QtdCompra = c.QtdCompra,
                                                   IdProdutoCarrinho = c.Id,
                                                   Url = p.Url,
                                                   DataCompra = co.DataCompra

                                               }).AsNoTracking().ToListAsync();

                return produtosCarrinhoUsuario;
            }                                 
        }

        public async Task<Produto> ObterProdutoCarrinho(int idProdutoCarrinho)
        {
            using (var banco = new Contexto(_dbContextOptions))
            {
                var produtosCarrinhoUsuario = await (from p in banco.Produto
                                                     join c in banco.CompraUsuario on p.Id equals c.IdProduto
                                                     where c.Id.Equals(idProdutoCarrinho) && c.Estado == EstadoCompra.Produto_Carrinho
                                                     select new Produto
                                                     {
                                                         Id = p.Id,
                                                         Nome = p.Nome,
                                                         Descricao = p.Descricao,
                                                         Observacao = p.Observacao,
                                                         Valor = p.Valor,
                                                         QtdCompra = c.QtdCompra,
                                                         IdProdutoCarrinho = c.Id,
                                                         Url = p.Url

                                                     }).AsNoTracking().FirstOrDefaultAsync();

                return produtosCarrinhoUsuario;
            }
        }
    }
}
