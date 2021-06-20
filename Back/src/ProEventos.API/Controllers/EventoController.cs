﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Persistence.Contextos;
using ProEventos.Domain;
using ProEventos.Application.Interfaces;
using Microsoft.AspNetCore.Http;

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
                    return NotFound("Nenhum evento encontrado");
                
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
                    return NotFound("Nenhum evento encontrado");
                
                return Ok(evento);
            }
            catch (Exception e)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar o evento procurado. Erro: {e.Message}");
            }
        }

        [HttpGet("tema/{tema}")]
        public async Task<IActionResult> GetEventoByTema(string tema)
        {
            try
            {
                var eventos = await _eventoService.GetEventosByTemaAsync(tema, true);
                if (eventos == null)
                    return NotFound("Nenhum evento encontrado");
                
                return Ok(eventos);
            }
            catch (Exception e)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {e.Message}");
            }
        }        
        [HttpPost]
        public async Task<IActionResult> AddEvento(Evento model)
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
        public async Task<IActionResult> UpdateEvento(int id, Evento model)
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
                if (await _eventoService.DeleteEvento(id))
                    return Ok("Evento removido com sucesso");
                
                return BadRequest("Evento não removido");
            }
            catch (Exception e)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar remover evento. Erro: {e.Message}");
            }
        }
    }
}
