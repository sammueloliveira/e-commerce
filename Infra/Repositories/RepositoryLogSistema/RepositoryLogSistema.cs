using Domain.Entities;
using Domain.Interfaces.InterfaceLogSistema;
using Infra.Repositories.RepositoryGeneric;

namespace Infra.Repositories.RepositoryLogSistema
{
    public class RepositoryLogSistema : RepositoryGeneric<LogSistema>, ILogSistema
    {
    }
}
