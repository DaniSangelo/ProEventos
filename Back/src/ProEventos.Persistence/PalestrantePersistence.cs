using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Interfaces;
using ProEventos.Persistence.Contextos;

namespace ProEventos.Persistence
{
    public class PalestrantePersistence : IPalestrantePersistence
    {
        private readonly ProEventosContext _context;

        public PalestrantePersistence(ProEventosContext context)
        {
            _context = context;
        }

        public async Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(palestrante => palestrante.RedesSociais);

            if (includeEventos){
                query = query
                    .Include(palestrante => palestrante.PalestrantesEventos)
                    .ThenInclude(palestrante => palestrante.Evento);
            }
            query = query.OrderBy(palestrante => palestrante.Id)
                .Where(palestrante => palestrante.Id == palestranteId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Palestrante[]> GetPalestrantesAsync(bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(palestrante => palestrante.RedesSociais);

            if (includeEventos){
                query = query
                    .Include(palestrante => palestrante.PalestrantesEventos)
                    .ThenInclude(palestrante => palestrante.Evento);
            }
            query = query.OrderBy(palestrante => palestrante.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante[]> GetPalestrantesByNomeAsync(string nome, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(palestrante => palestrante.RedesSociais);

            if (includeEventos){
                query = query
                    .Include(palestrante => palestrante.PalestrantesEventos)
                    .ThenInclude(palestrante => palestrante.Evento);
            }
            query = query.OrderBy(palestrante => palestrante.Id)
                .Where(palestrante => palestrante.Nome == nome);

            return await query.ToArrayAsync();
        }
    }
}