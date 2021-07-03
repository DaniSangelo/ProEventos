using System;
using System.Threading.Tasks;
using AutoMapper;
using ProEventos.Application.DTO;
using ProEventos.Application.Interfaces;
using ProEventos.Domain;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Application
{
    public class EventoService : IEventoService
    {
        private readonly IGeralPersistence _geralPersistence;
        private readonly IEventoPersistence _eventoPersistence;
        private readonly IMapper _mapper;
        public EventoService(IGeralPersistence geralPersistence, IEventoPersistence eventoPersistence, IMapper mapper)
        {
            _eventoPersistence = eventoPersistence;
            _geralPersistence = geralPersistence;
            _mapper = mapper;

        }
        public async Task<EventoDTO> AddEvento(EventoDTO model)
        {
            try
            {
                var evento = _mapper.Map<Evento>(model);
                _geralPersistence.Add<Evento>(evento);
                if (await _geralPersistence.SaveChangesAsync()){
                    var result = await _eventoPersistence.GetEventoByIdAsync(evento.Id, false);
                    return _mapper.Map<EventoDTO>(result);
                }

                return null;
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao adicionar evento: {e.Message}");
            }
        }
        public async Task<EventoDTO> UpdateEvento(int eventoId, EventoDTO model)
        {
            try
            {
                var evento = await _eventoPersistence.GetEventoByIdAsync(eventoId, false);
                if (evento == null)
                    return null;

                model.Id = evento.Id;

                _mapper.Map(model, evento);

                _geralPersistence.Update<Evento>(evento);
                if (await _geralPersistence.SaveChangesAsync()){
                    var result = await _eventoPersistence.GetEventoByIdAsync(evento.Id, false);
                    return _mapper.Map<EventoDTO>(result);
                }

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

        public async Task<EventoDTO[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersistence.GetAllEventosAsync(includePalestrantes);
                if (eventos == null)
                    return null;

                var results = _mapper.Map<EventoDTO[]>(eventos);

                return results;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<EventoDTO> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            try
            {
                var evento = await _eventoPersistence.GetEventoByIdAsync(eventoId, includePalestrantes);
                if (evento == null)
                    return null;

                var result = _mapper.Map<EventoDTO>(evento);

                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<EventoDTO[]> GetEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersistence.GetEventosByTemaAsync(tema, includePalestrantes);
                if (eventos == null)
                    return null;

                var results = _mapper.Map<EventoDTO[]>(eventos);

                return results;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}