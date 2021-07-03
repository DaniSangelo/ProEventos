using System.Threading.Tasks;
using ProEventos.Application.DTO;
namespace ProEventos.Application.Interfaces
{
    public interface IEventoService
    {
        Task<EventoDTO> AddEvento(EventoDTO model);    
        Task<EventoDTO> UpdateEvento(int eventoId, EventoDTO model);    
        Task<bool> DeleteEvento(int eventoId);

        Task<EventoDTO> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false);
        Task<EventoDTO[]> GetEventosByTemaAsync(string tema, bool includePalestrantes = false);
        Task<EventoDTO[]> GetAllEventosAsync(bool includePalestrantes = false);          
    }
}