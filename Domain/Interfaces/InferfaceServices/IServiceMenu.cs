using Domain.Entities;

namespace Domain.Interfaces.InferfaceServices
{
    public interface IServiceMenu
    {
        Task<List<Menu>> MontarMenuPorPerfil(string userID);
    }
}
