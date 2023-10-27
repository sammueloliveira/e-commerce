using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.InterfaceGeneric;

namespace Domain.Interfaces.InterfaceCompraUsuario
{
    public interface ICompraUsuario :IGeneric<CompraUsuario>
    {
        public Task<int> QuantidadeProdutoCarrinhoUsuario(string userId);
        public Task<CompraUsuario> ProdutosCompradosPorEstado(string userId, EstadoCompra estado, int? idCompra = null);
        public Task<bool> ConfirmaCompraCarrinhoUsuario(string userId);
        public Task<List<CompraUsuario>> MinhasComprasPorEstado(string userId, EstadoCompra estado);
    }
}
