using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence.Interfaces
{
    public interface IPalestrantePersistence
    {
        Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool includeEventos);
        Task<Palestrante[]> GetPalestrantesByNomeAsync(string nome, bool includeEventos);
        Task<Palestrante[]> GetPalestrantesAsync(bool includeEventos);
    }
}