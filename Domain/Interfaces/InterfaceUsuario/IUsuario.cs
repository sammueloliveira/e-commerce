using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.InterfaceGeneric;

namespace Domain.Interfaces.InterfaceUsuario
{
    public interface IUsuario : IGeneric<ApplicationUser>
    {
        Task<ApplicationUser> ObterUsuarioPeloID(string userID);
        Task AtualizarTipoUsuario(string userID, TipoUsuario tipoUsuario);
    }
}
