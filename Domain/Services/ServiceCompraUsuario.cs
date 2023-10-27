using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.InferfaceServices;
using Domain.Interfaces.InterfaceCompra;
using Domain.Interfaces.InterfaceCompraUsuario;

namespace Domain.Services
{
    public class ServiceCompraUsuario : IServiceCompraUsuario
    {

        private readonly ICompraUsuario _IcompraUsuario;
        private readonly ICompra _Icompra;

        public ServiceCompraUsuario(ICompraUsuario compraUsuario, ICompra compra)
        {
            _IcompraUsuario = compraUsuario;
            _Icompra = compra;
        }

        public async Task AdicionarProdutoCarrinho(string userId, CompraUsuario compraUsuario)
        {
            var compra = await _Icompra.CompraPorEstado(userId, EstadoCompra.Produto_Carrinho);
            if (compra == null)
            {
                compra = new Compra
                {
                    UserId = userId,
                    Estado = EstadoCompra.Produto_Carrinho
                };
                await _Icompra.Add(compra);
            }

            if (compra.Id > 0)
            {
                compraUsuario.IdCompra = compra.Id;
                await _IcompraUsuario.Add(compraUsuario);
            }
        }
         public async Task<CompraUsuario> CarrinhoCompras(string userId)
        {
            return await _IcompraUsuario.ProdutosCompradosPorEstado(userId, EstadoCompra.Produto_Carrinho);
        }

        public async Task<List<CompraUsuario>> MinhasCompras(string userId)
        {
            return await _IcompraUsuario.MinhasComprasPorEstado(userId, EstadoCompra.Produto_Comprado);
        }

        public async Task<CompraUsuario> ProdutosComprados(string userId, int? idCompra = null)
        {
            return await _IcompraUsuario.ProdutosCompradosPorEstado(userId, EstadoCompra.Produto_Comprado, idCompra);
        }
    }
}
