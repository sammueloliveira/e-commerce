using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.InferfaceServices;
using Domain.Interfaces.InterfaceUsuario;

namespace Application.OpenApp
{
    public class UsuarioApp : IUsuarioApp
    {
        private readonly IUsuario _usuario;
        private readonly IUsuarioService _usuarioService;

        public UsuarioApp(IUsuario usuario, IUsuarioService usuarioService)
        {
            _usuario = usuario;
            _usuarioService = usuarioService;
        }

        public async Task AtualizarTipoUsuario(string userID, TipoUsuario tipoUsuario)
        {
            await _usuario.AtualizarTipoUsuario(userID, tipoUsuario);

        }

        public async Task<ApplicationUser> ObterUsuarioPeloID(string userID)
        {
           return await _usuario.ObterUsuarioPeloID(userID);
        }

        public async Task<List<ApplicationUser>> ListarUsuarioSomenteParaAdministradores(string userID)
        {
            return await _usuarioService.ListarUsuarioSomenteParaAdministradores(userID);
        }
        public async Task Add(ApplicationUser objeto)
        {
            await _usuario.Add(objeto);
        }

        public async Task Delete(ApplicationUser objeto)
        {
            await _usuario.Delete(objeto);
        }

        public async Task<ApplicationUser> GetEntityById(int Id)
        {
            return await _usuario.GetEntityById(Id);
        }
        public async Task<List<ApplicationUser>> List()
        {
            return await _usuario.List();
        }

        public async Task Update(ApplicationUser objeto)
        {
             await _usuario.Update(objeto);
        }

       
    }
}
