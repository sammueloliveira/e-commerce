using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.InterfaceGeneric;

namespace Domain.Interfaces.InterfaceCompra
{
    public interface ICompra : IGeneric<Compra>
    {
        public Task<Compra> CompraPorEstado(string userId, EstadoCompra estado);
    }
}
