using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence.Interfaces
{
    public interface IEventoPersistence
    {
        Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes);
        Task<Evento[]> GetEventosByTemaAsync(string tema, bool includePalestrantes);
        Task<Evento[]> GetAllEventosAsync(bool includePalestrantes);
    }
}