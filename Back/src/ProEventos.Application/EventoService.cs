using System;
using System.Threading.Tasks;
using ProEventos.Application.Interfaces;
using ProEventos.Domain;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Application
{
    public class EventoService : IEventoService
    {
        private readonly IGeralPersistence _geralPersistence;
        private readonly IEventoPersistence _eventoPersistence;
        public EventoService(IGeralPersistence geralPersistence, IEventoPersistence eventoPersistence)
        {
            _eventoPersistence = eventoPersistence;
            _geralPersistence = geralPersistence;

        }
        public async Task<Evento> AddEvento(Evento model)
        {
            try
            {
                _geralPersistence.Add<Evento>(model);
                if(await _geralPersistence.SaveChangesAsync())
                    return await _eventoPersistence.GetEventoByIdAsync(model.Id, false);
                
                return null;
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao adicionar evento: {e.Message}");
            }
        }
        public async Task<Evento> UpdateEvento(int eventoId, Evento model)
        {
            try
            {
                var evento = await _eventoPersistence.GetEventoByIdAsync(eventoId, false);
                if (evento == null)
                    return null;
                
                model.Id = evento.Id;

                _geralPersistence.Update(model);
                if(await _geralPersistence.SaveChangesAsync())
                    return await _eventoPersistence.GetEventoByIdAsync(model.Id, false);
                
                return null;
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao atualizar evento: {e.Message}");
            }
        }
        public async Task<bool> DeleteEvento(int eventoId)
        {
            try
            {
                var evento = await _eventoPersistence.GetEventoByIdAsync(eventoId, false);
                if (evento == null)
                    throw new Exception("O evento que se deseja excluir não foi encontrado.");
                
                _geralPersistence.Delete<Evento>(evento);
                return await _geralPersistence.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao remover evento: {e.Message}");
            }
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersistence.GetAllEventosAsync(includePalestrantes);
                if (eventos == null)
                    return null;

                return eventos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersistence.GetEventoByIdAsync(eventoId, includePalestrantes);
                if (eventos == null)
                    return null;

                return eventos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Evento[]> GetEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersistence.GetEventosByTemaAsync(tema, includePalestrantes);
                if (eventos == null)
                    return null;

                return eventos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}