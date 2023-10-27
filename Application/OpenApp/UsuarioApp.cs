using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.InferfaceServices;
using Domain.Interfaces.InterfaceUsuario;

namespace Application.OpenApp
{
    public class UsuarioApp : IUsuarioApp
    {
        private readonly IUsuario _Iusuario;
        private readonly IServiceUsuario _IserviceUsuario;

        public UsuarioApp(IUsuario usuario, IServiceUsuario service)
        {
            _Iusuario = usuario;
            _IserviceUsuario = service;
        }

        public async Task AtualizarTipoUsuario(string userID, TipoUsuario tipoUsuario)
        {
            await _Iusuario.AtualizarTipoUsuario(userID, tipoUsuario);

        }

        public async Task<ApplicationUser> ObterUsuarioPeloID(string userID)
        {
           return await _Iusuario.ObterUsuarioPeloID(userID);
        }

        public async Task<List<ApplicationUser>> ListarUsuarioSomenteParaAdministradores(string userID)
        {
            return await _IserviceUsuario.ListarUsuarioSomenteParaAdministradores(userID);
        }
        public async Task Add(ApplicationUser objeto)
        {
            await _Iusuario.Add(objeto);
        }

        public async Task Delete(ApplicationUser objeto)
        {
            await _Iusuario.Delete(objeto);
        }

        public async Task<ApplicationUser> GetEntityById(int Id)
        {
            return await _Iusuario.GetEntityById(Id);
        }
        public async Task<List<ApplicationUser>> List()
        {
            return await _Iusuario.List();
        }

        public async Task Update(ApplicationUser objeto)
        {
             await _Iusuario.Update(objeto);
        }

       
    }
}
