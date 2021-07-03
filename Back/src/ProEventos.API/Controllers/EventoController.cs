using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using ProEventos.Application.DTO;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        private readonly IEventoService _eventoService;
        public EventoController(IEventoService eventoService)
        {
            _eventoService = eventoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEventos()
        {
            try
            {
                var eventos = await _eventoService.GetAllEventosAsync(true);
                if (eventos == null)
                    return NoContent();
           
                return Ok(eventos);
            }
            catch (Exception e)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {e.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventoById(int id)
        {
            try
            {
                var evento = await _eventoService.GetEventoByIdAsync(id, true);
                if (evento == null)
                    return NoContent();
                
                return Ok(evento);
            }
            catch (Exception e)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar o evento procurado. Erro: {e.Message}");
            }
        }

        [HttpGet("{tema}/tema")]
        public async Task<IActionResult> GetEventoByTema(string tema)
        {
            try
            {
                var eventos = await _eventoService.GetEventosByTemaAsync(tema, true);
                if (eventos == null)
                    return NoContent();
                
                return Ok(eventos);
            }
            catch (Exception e)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {e.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddEvento(EventoDTO model)
        {
            try
            {
                var evento = await _eventoService.AddEvento(model);
                if (evento == null)
                    return BadRequest("Erro ao tentar adicionar evento.");
                
                return Ok(evento);
            }
            catch (Exception e)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar evento. Erro: {e.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvento(int id, EventoDTO model)
        {
            try
            {
                var evento = await _eventoService.UpdateEvento(id, model);
                if (evento == null)
                    return BadRequest("Erro ao tentar atualizar evento.");
                
                return Ok(evento);
            }
            catch (Exception e)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar atualizar evento. Erro: {e.Message}");
            }
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvento(int id)
        {
            try
            {
                return await _eventoService.DeleteEvento(id) ? Ok("Evento removido com sucesso") : BadRequest("Evento não removido");
            }
            catch (Exception e)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar remover evento. Erro: {e.Message}");
            }
        }
    }
}
