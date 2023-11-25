using Application.Interfaces;
using Application.OpenApp;
using Domain.Interfaces.InferfaceServices;
using Domain.Interfaces.InterfaceCompra;
using Domain.Interfaces.InterfaceCompraUsuario;
using Domain.Interfaces.InterfaceGeneric;
using Domain.Interfaces.InterfaceLogSistema;
using Domain.Interfaces.InterfaceProduto;
using Domain.Interfaces.InterfaceUsuario;
using Domain.Services;
using Infra.Repositories.RepositoryCompra;
using Infra.Repositories.RepositoryCompraUsuario;
using Infra.Repositories.RepositoryGeneric;
using Infra.Repositories.RepositoryLogSistema;
using Infra.Repositories.RepositoryProduto;
using Infra.Repositories.RepositoyUsuario;
using Microsoft.Extensions.DependencyInjection;

namespace HelpConfig
{
    public static class HelpStartup
    {

        public static void ConfigureSingleton(IServiceCollection services)
        {
            // INTERFACE E REPOSITORIO
            services.AddSingleton(typeof(IGeneric<>), typeof(RepositoryGeneric<>));
            services.AddSingleton<IProduto, RepositoryProduto>();
            services.AddSingleton<ICompraUsuario, RepositoryCompraUsuario>();
            services.AddSingleton<ICompra, RepositoryCompra>();
            services.AddSingleton<ILogSistema, RepositoryLogSistema>();
            services.AddSingleton<IUsuario, RepositoryUsuario>();

            // INTERFACE APLICAÇÃO
            services.AddSingleton<IProdutoApp, ProdutoApp>();
            services.AddSingleton<ICompraUsuarioApp, CompraUsuarioApp>();
            services.AddSingleton<ICompraApp, CompraApp>();
            services.AddSingleton<ILogSistemaApp, LogSistemaApp>();
            services.AddSingleton<IUsuarioApp, UsuarioApp>();
            services.AddSingleton<IMenuApp, MenuApp>();
            

            // SERVIÇO DOMINIO
            services.AddSingleton<IProdutoService, ProdutoService>();
            services.AddSingleton<ICompraUsuarioService, CompraUsuarioService>();
            services.AddSingleton<IUsuarioService, UsuarioService>();
            services.AddSingleton<IMenuService, MenuService>();
           
        }

    }
}
