using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.InterfaceUsuario;
using Infra.Data;
using Infra.Repositories.RepositoryGeneric;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories.RepositoyUsuario
{
    public class RepositoryUsuario : RepositoryGeneric<ApplicationUser>, IUsuario
    {
        private readonly DbContextOptions<Contexto> _dbContextOptions;

        public RepositoryUsuario()
        {
            _dbContextOptions = new DbContextOptions<Contexto>();
        }
        public async Task AtualizarTipoUsuario(string userID, TipoUsuario tipoUsuario)
        {
            using (var banco = new Contexto(_dbContextOptions))
            {
                var usuario = await banco.ApplicationUser.FirstOrDefaultAsync(u => u.Id.Equals(userID));
                if(usuario != null)
                {
                    usuario.Tipo = tipoUsuario;
                    banco.ApplicationUser.Update(usuario);
                    await banco.SaveChangesAsync();
                }
            }
        }

        public async Task<ApplicationUser> ObterUsuarioPeloID(string userID)
        {
           using(var banco = new Contexto(_dbContextOptions))
            {
                return await banco.ApplicationUser.FirstOrDefaultAsync(u => u.Id.Equals(userID));
            }
        }
    }
}
