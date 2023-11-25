using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.InferfaceServices;
using Domain.Interfaces.InterfaceUsuario;

namespace Domain.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuario _Isuario;

        public UsuarioService(IUsuario usuario)
        {
            _Isuario = usuario;
        }

        public async Task<List<ApplicationUser>> ListarUsuarioSomenteParaAdministradores(string userID)
        {
            var usuario = await _Isuario.ObterUsuarioPeloID(userID);
            if(usuario != null && usuario.Tipo == TipoUsuario.Administrador)
            {
                return await _Isuario.List();
            }

            return new List<ApplicationUser>();
        }
    }
}
