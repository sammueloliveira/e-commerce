using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.InferfaceServices;
using Domain.Interfaces.InterfaceUsuario;

namespace Domain.Services
{
     public class MenuService : IMenuService
    {
        private readonly IUsuario _Iusuario;

        public MenuService(IUsuario usuario)
        {
            _Iusuario = usuario;
        }

        public async Task<List<Menu>> MontarMenuPorPerfil(string userID)
        {
            var retorno = new List<Menu>();
            retorno.Add(new Menu { Controller = "Home", Action = "Index", Descricao = "Loja Virtual" });

            if (!string.IsNullOrWhiteSpace(userID))
            {
                
                retorno.Add(new Menu { Controller = "Produto", Action = "Index", Descricao = "Meus Produtos" });
                retorno.Add(new Menu { Controller = "CompraUsuario", Action = "MinhasCompras", Descricao = "Minhas Compras" });
               

                var usuario = await _Iusuario.ObterUsuarioPeloID(userID);
                if (usuario != null && usuario.Tipo != null)
                {
                    switch ((TipoUsuario)usuario.Tipo)
                    {
                        case TipoUsuario.Administrador:
                            retorno.Add(new Menu { Controller = "LogSistema", Action = "Index", Descricao = "Logs" });
                            retorno.Add(new Menu { Controller = "Usuario", Action = "ListarUsuarios", Descricao = "Usuários" });
                            break;
                        case TipoUsuario.Comum:
                            break;
                        default:
                            break;
                    }
                }

                retorno.Add(new Menu { Controller = "Produto", Action = "ListarProdutosCarrinhoUsuario", Descricao = "", IdCampo = "qtdCarrinho", UrlImagem = "../img/carrinho.png" });

            }

            return retorno;
        }
    }
    
}
