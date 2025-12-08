using ApiNook.DataContexts;
using ApiNook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiNook.Controllers
{
    [Route("eventos")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EventoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetEventos([FromQuery] string? titulo)
        {
            var query = _context.Eventos.AsQueryable();

            if (!string.IsNullOrEmpty(titulo))
                query = query.Where(e => e.Titulo.Contains(titulo));

            return Ok(await query.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventoPorId(int id)
        {
            var evento = await _context.Eventos
                .Include(e => e.Participacoes)
                .ThenInclude(p => p.Cliente)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (evento is null)
                return NotFound();

            return Ok(evento);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] Evento novo)
        {
            _context.Eventos.Add(novo);
            await _context.SaveChangesAsync();
            return Created("", novo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] Evento dados)
        {
            var evento = await _context.Eventos.FindAsync(id);
            if (evento is null)
                return NotFound();

            evento.Titulo = dados.Titulo;
            evento.Descricao = dados.Descricao;
            evento.Data = dados.Data;
            evento.Vagas = dados.Vagas;

            await _context.SaveChangesAsync();
            return Ok(evento);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            var evento = await _context.Eventos
                .Include(e => e.Participacoes)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (evento is null)
                return NotFound();

            if (evento.Participacoes.Any())
                return BadRequest("Este evento possui participações e não pode ser removido.");

            _context.Eventos.Remove(evento);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
