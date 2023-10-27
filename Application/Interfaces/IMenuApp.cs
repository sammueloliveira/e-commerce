using Domain.Entities;

namespace Application.Interfaces
{
     public interface IMenuApp 
    {
        Task<List<Menu>> MontarMenuPorPerfil(string userID);
    }
}
