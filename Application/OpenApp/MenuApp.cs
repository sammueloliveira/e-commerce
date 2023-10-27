using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces.InferfaceServices;
using Domain.Services;

namespace Application.OpenApp
{
    public class MenuApp : IMenuApp
    {
        private readonly IServiceMenu _IserviceMenu;

       
        public MenuApp(IServiceMenu menu)
        {
            _IserviceMenu = menu;
        }

        public async Task<List<Menu>> MontarMenuPorPerfil(string userID)
        {
            return await _IserviceMenu.MontarMenuPorPerfil(userID);
        }

      
    }
}
