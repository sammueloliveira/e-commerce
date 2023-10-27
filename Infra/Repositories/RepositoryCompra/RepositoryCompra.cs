using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.InterfaceCompra;
using Infra.Data;
using Infra.Repositories.RepositoryGeneric;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories.RepositoryCompra
{
    public class RepositoryCompra : RepositoryGeneric<Compra>, ICompra
    {
        private readonly DbContextOptions<Contexto> _dbContextOptions;

        public RepositoryCompra()
        {
            _dbContextOptions = new DbContextOptions<Contexto>();
        }
        public async Task<Compra> CompraPorEstado(string userId, EstadoCompra estado)
        {
            using (var banco = new Contexto(_dbContextOptions))
            {
                return await banco.Compra.FirstOrDefaultAsync(c => c.Estado == estado && c.UserId == userId);
            }
        }
    }
}
