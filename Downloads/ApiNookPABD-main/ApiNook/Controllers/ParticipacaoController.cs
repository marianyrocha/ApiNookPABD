using ApiNook.DataContexts;
using ApiNook.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiNook.Controllers
{
    [Route("participacoes")]
    [ApiController]
   // [Authorize]
    public class ParticipacaoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ParticipacaoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetParticipacoes()
        {
            var lista = await _context.Participacoes
                .Include(p => p.Cliente)
                .Include(p => p.Evento)
                .ToListAsync();

            return Ok(lista);
        }

        [HttpPost("evento/{eventoId}/cliente/{clienteId}")]
        public async Task<IActionResult> Registrar(int eventoId, int clienteId)
        {
            var evento = await _context.Eventos
                .Include(e => e.Participacoes)
                .FirstOrDefaultAsync(e => e.Id == eventoId);

            if (evento is null) return NotFound("Evento não encontrado.");

            var cliente = await _context.Clientes.FindAsync(clienteId);
            if (cliente is null) return NotFound("Cliente não encontrado.");

            if (evento.Vagas <= 0)
                return BadRequest("O evento está sem vagas disponíveis.");

            if (_context.Participacoes.Any(p => p.EventoId == eventoId && p.ClienteId == clienteId))
                return BadRequest("O cliente já está inscrito neste evento.");

            var participacao = new ParticipacaoEvento
            {
                ClienteId = clienteId,
                EventoId = eventoId,
                DataRegistro = DateTime.UtcNow
            };

            evento.Vagas--;

            await _context.Participacoes.AddAsync(participacao);
            await _context.SaveChangesAsync();

            return Created("", participacao);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            var participacao = await _context.Participacoes
                .Include(p => p.Evento)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (participacao is null) return NotFound();

            participacao.Evento.Vagas++;

            _context.Participacoes.Remove(participacao);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
