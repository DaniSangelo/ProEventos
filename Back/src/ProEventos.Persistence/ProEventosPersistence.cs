using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;

namespace ProEventos.Persistence
{
    public class ProEventosPersistence : IProEventosPersistence
    {
        private readonly ProEventosContext _context;

        public ProEventosPersistence(ProEventosContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void DeleteRange<T>(T[] entityArray) where T : class
        {
            _context.RemoveRange(entityArray);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<Evento> GetAllEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(evento => evento.Lotes)
                .Include(evento => evento.RedesSociais);
            if (includePalestrantes){
                query = query
                    .Include(evento => evento.PalestrantesEventos)
                    .ThenInclude(pe => pe.Palestrante);
            }
            query = query.OrderBy(evento => evento.Id)
                .Where(evento => evento.Id == eventoId);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(evento => evento.Lotes)
                .Include(evento => evento.RedesSociais);
            if (includePalestrantes){
                query = query
                    .Include(evento => evento.PalestrantesEventos)
                    .ThenInclude(pe => pe.Palestrante);
            }
            query = query.OrderBy(evento => evento.Id);
            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(evento => evento.Lotes)
                .Include(evento => evento.RedesSociais);
            if (includePalestrantes){
                query = query
                    .Include(evento => evento.PalestrantesEventos)
                    .ThenInclude(pe => pe.Palestrante);
            }
            query = query.OrderBy(evento => evento.Id)
                .Where(evento => evento.Tema.ToLower().Contains(tema.ToLower()));
            return await query.ToArrayAsync();
        }

        public async Task<Palestrante> GetAllPalestranteByIdAsync(int palestranteId, bool includeEventos = false)
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

        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false)
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

        public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos = false)
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