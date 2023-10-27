using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces.InterfaceLogSistema;

namespace Application.OpenApp
{
    public class LogSistemaApp : ILogSistemaApp
    {
        private readonly ILogSistema _IlogSistema;

        public LogSistemaApp(ILogSistema logSistema)
        {
            _IlogSistema = logSistema;
        }
        public async Task Add(LogSistema objeto)
        {
             await _IlogSistema.Add(objeto);
        }

        public async Task Delete(LogSistema objeto)
        {
             await _IlogSistema.Delete(objeto);
        }

        public async Task<LogSistema> GetEntityById(int Id)
        {
            return await _IlogSistema.GetEntityById(Id);
        }

        public async Task<List<LogSistema>> List()
        {
            return await _IlogSistema.List();
        }

        public async Task Update(LogSistema objeto)
        {
            await _IlogSistema.Update(objeto);
        }
    }
}
