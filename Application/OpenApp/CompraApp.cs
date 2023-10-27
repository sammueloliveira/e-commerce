using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces.InterfaceCompra;

namespace Application.OpenApp
{
    public class CompraApp : ICompraApp
    {
        private readonly ICompra _Icompra;

        public CompraApp(ICompra compra)
        {
            _Icompra = compra;
        }

        public async Task Add(Compra objeto)
        {
           await _Icompra.Add(objeto);
        }

        public async Task Delete(Compra objeto)
        {
            await _Icompra.Delete(objeto);
        }

        public async Task<Compra> GetEntityById(int Id)
        {
            return await _Icompra.GetEntityById(Id);
        }

        public async Task<List<Compra>> List()
        {
            return await _Icompra.List();
        }

        public async Task Update(Compra objeto)
        {
            await _Icompra.Update(objeto);
        }
    }
}
