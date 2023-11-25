using Domain.Entities;

namespace Domain.Interfaces.InferfaceServices
{
    public interface IMenuService
    {
        Task<List<Menu>> MontarMenuPorPerfil(string userID);
    }
}
