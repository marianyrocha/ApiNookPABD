using ApiNook.DataContexts;
using ApiNook.Models;
using ApiNook.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiNook.Controllers
{
    [Route("reservas")]
    [ApiController]
    public class ReservaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReservaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarTodas()
        {
            var reservas = await _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.Livro)
                .ToListAsync();

            return Ok(reservas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var reserva = await _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.Livro)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reserva is null)
                return NotFound();

            return Ok(reserva);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] ReservaDto dto)
        {
            var cliente = await _context.Clientes.FindAsync(dto.ClienteId);
            var livro = await _context.Livros.FindAsync(dto.LivroId);

            if (cliente is null || livro is null)
                return BadRequest("Cliente ou Livro não encontrado.");

            var reserva = new Reserva
            {
                ClienteId = dto.ClienteId,
                LivroId = dto.LivroId,
                Status = dto.Status
            };

            _context.Reservas.Add(reserva);
            await _context.SaveChangesAsync();

            return Created("", reserva);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] ReservaDto dto)
        {
            var reserva = await _context.Reservas.FindAsync(id);

            if (reserva is null)
                return NotFound();

            reserva.Status = dto.Status;

            _context.Reservas.Update(reserva);
            await _context.SaveChangesAsync();

            return Ok(reserva);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);

            if (reserva is null)
                return NotFound();

            _context.Reservas.Remove(reserva);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
