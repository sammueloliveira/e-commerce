using Domain.Entities;

namespace Domain.Interfaces.InferfaceServices
{
    public interface IServiceUsuario
    {
        Task<List<ApplicationUser>> ListarUsuarioSomenteParaAdministradores(string userID);
    }
}
