using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.InterfaceCompraUsuario;
using Infra.Data;
using Infra.Repositories.RepositoryGeneric;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories.RepositoryCompraUsuario
{
    public class RepositoryCompraUsuario : RepositoryGeneric<CompraUsuario>, ICompraUsuario
    {
        private readonly DbContextOptions<Contexto> _dbContextOptions;

        public RepositoryCompraUsuario()
        {
            _dbContextOptions = new DbContextOptions<Contexto>();
        }

        public async Task<bool> ConfirmaCompraCarrinhoUsuario(string userId)
        {
            try
            {
                using (var banco = new Contexto(_dbContextOptions))
                {
                    var compraUsuario = new CompraUsuario();
                    compraUsuario.ListaProdutos = new List<Produto>();

                    var produtosCarrinhoUsuario = await (from p in banco.Produto
                                                         join c in banco.CompraUsuario on p.Id equals c.IdProduto
                                                         where c.UserId.Equals(userId) && c.Estado == EstadoCompra.Produto_Carrinho
                                                         select c).AsNoTracking().ToListAsync();

                    produtosCarrinhoUsuario.ForEach(p =>
                    {
                        compraUsuario.IdCompra = p.IdCompra;
                        p.Estado = EstadoCompra.Produto_Comprado;
                    });


                    var compra = await banco.Compra.AsNoTracking().FirstOrDefaultAsync(c => c.Id == compraUsuario.IdCompra);
                    if (compra != null)
                    {
                        compra.Estado = EstadoCompra.Produto_Comprado;
                    }

                    banco.Update(compra);
                    banco.UpdateRange(produtosCarrinhoUsuario);
                    await banco.SaveChangesAsync();

                    return true;
                }
            }
            catch (Exception erro)
            {
                return false;
            }

        }
        public async Task<List<CompraUsuario>> MinhasComprasPorEstado(string userId, EstadoCompra estado)
        {
            var retorno = new List<CompraUsuario>();

            using (var banco = new Contexto(_dbContextOptions))
            {
                var comprasUsuario = await banco.Compra
                    .Where(co => co.Estado == estado && co.UserId.Equals(userId)).ToListAsync();

                foreach (var item in comprasUsuario)
                {
                    var compraUsuario = new CompraUsuario();
                    compraUsuario.ListaProdutos = new List<Produto>();

                    var produtosCarrinhoUsuario = await (from p in banco.Produto
                                                         join c in banco.CompraUsuario on p.Id equals c.IdProduto
                                                         where c.UserId.Equals(userId) && c.Estado == estado && c.IdCompra == item.Id
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
                                                             DataCompra = item.DataCompra
                                                         }).AsNoTracking().ToListAsync();


                    compraUsuario.ListaProdutos = produtosCarrinhoUsuario;
                    compraUsuario.ApplicationUser = await banco.ApplicationUser.FirstOrDefaultAsync(u => u.Id.Equals(userId));
                    compraUsuario.QuantidadeProdutos = produtosCarrinhoUsuario.Count();
                    compraUsuario.EnderecoCompleto = string.Concat(compraUsuario.ApplicationUser.Endereco, " - ", compraUsuario.ApplicationUser.ComplementoEndereco, " - CEP: ", compraUsuario.ApplicationUser.CEP);
                    compraUsuario.ValorTotal = produtosCarrinhoUsuario.Sum(v => v.Valor);
                    compraUsuario.Estado = estado;
                    compraUsuario.Id = item.Id;

                    retorno.Add(compraUsuario);
                }

                return retorno;

            }
        }

        public async Task<CompraUsuario> ProdutosCompradosPorEstado(string userId, EstadoCompra estado, int? idCompra = null)
        {
            using (var banco = new Contexto(_dbContextOptions))
            {
                var compraUsuario = new CompraUsuario();
                compraUsuario.ListaProdutos = new List<Produto>();

                var produtosCarrinhoUsuario = await (from p in banco.Produto
                                                     join c in banco.CompraUsuario on p.Id equals c.IdProduto
                                                     join co in banco.Compra on c.IdCompra equals co.Id
                                                     where c.UserId.Equals(userId) && c.Estado == estado &&
                                                     co.UserId.Equals(userId) && co.Estado == estado
                                                     && (idCompra == null || co.Id == idCompra)
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

                compraUsuario.ListaProdutos = produtosCarrinhoUsuario;
                compraUsuario.ApplicationUser = await banco.ApplicationUser.FirstOrDefaultAsync(u => u.Id.Equals(userId));
                compraUsuario.QuantidadeProdutos = produtosCarrinhoUsuario.Count();
                compraUsuario.EnderecoCompleto = string.Concat(compraUsuario.ApplicationUser.Endereco, " - ", compraUsuario.ApplicationUser.ComplementoEndereco, " - CEP: ", compraUsuario.ApplicationUser.CEP);
                compraUsuario.ValorTotal = produtosCarrinhoUsuario.Sum(v => v.Valor);
                compraUsuario.Estado = estado;
                return compraUsuario;


            }
        }




        public async Task<int> QuantidadeProdutoCarrinhoUsuario(string userId)
        {
            using (var banco = new Contexto(_dbContextOptions))
            {
                return await banco.CompraUsuario.CountAsync(c => c.UserId.Equals(userId) && c.Estado == EstadoCompra.Produto_Carrinho);
            }
        }
    }
}








