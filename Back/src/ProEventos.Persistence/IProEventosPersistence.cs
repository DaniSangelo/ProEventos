using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence
{
    public interface IProEventosPersistence
    {
        void Add<T>(T entity) where T: class;
        void Update<T>(T entity) where T: class;
        void Delete<T>(T entity) where T: class;
        void DeleteRange<T>(T[] entity) where T: class;
        Task<bool> SaveChangesAsync();

        Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes);
        Task<Evento[]> GetEventosByTemaAsync(string tema, bool includePalestrantes);
        Task<Evento[]> GetAllEventosAsync(bool includePalestrantes);
        
        Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool includeEventos);
        Task<Palestrante[]> GetPalestrantesByNomeAsync(string nome, bool includeEventos);
        Task<Palestrante[]> GetPalestrantesAsync(bool includeEventos);
    }
}