using Domain.Entities;

namespace Domain.Interfaces.InferfaceServices
{
    public interface IUsuarioService
    {
        Task<List<ApplicationUser>> ListarUsuarioSomenteParaAdministradores(string userID);
    }
}
