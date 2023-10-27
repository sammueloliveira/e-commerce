using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces
{
    public interface IUsuarioApp : IGenericApp<ApplicationUser>
    {
        Task<ApplicationUser> ObterUsuarioPeloID(string userID);
        Task AtualizarTipoUsuario(string userID, TipoUsuario tipoUsuario);
        Task<List<ApplicationUser>> ListarUsuarioSomenteParaAdministradores(string userID);
    }
}
