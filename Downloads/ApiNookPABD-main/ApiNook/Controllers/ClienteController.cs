using ApiNook.DataContexts;
using ApiNook.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiNook.Controllers
{
    [Route("clientes")]
    [ApiController]
//    [Authorize(Roles = "Admin,Funcionario")]
    public class ClienteController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClienteController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetClientes()
        {
            var clientes = await _context.Clientes
                .Include(c => c.Reservas)
                .Include(c => c.Participacoes)
                .ToListAsync();

            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientePorId(int id)
        {
            var cliente = await _context.Clientes
                .Include(c => c.Reservas)
                .Include(c => c.Participacoes)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (cliente is null)
                return NotFound("Cliente não encontrado.");

            return Ok(cliente);
        }

        [HttpPost]
       // [Authorize(Roles = "Admin,Funcionario")]
        public async Task<IActionResult> CriarCliente([FromBody] Cliente novo)
        {
            if (_context.Clientes.Any(c => c.Cpf == novo.Cpf))
                return BadRequest("Já existe um cliente com este CPF.");

            if (_context.Clientes.Any(c => c.Email == novo.Email))
                return BadRequest("Já existe um cliente com este email.");

            await _context.Clientes.AddAsync(novo);
            await _context.SaveChangesAsync();

            return Created("", novo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarCliente(int id, [FromBody] Cliente atualizacao)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente is null)
                return NotFound("Cliente não encontrado.");

            if (_context.Clientes.Any(c => c.Email == atualizacao.Email && c.Id != id))
                return BadRequest("Email já está em uso por outro cliente.");

            cliente.Nome = atualizacao.Nome;
            cliente.Cpf = atualizacao.Cpf;
            cliente.Email = atualizacao.Email;
            cliente.Telefone = atualizacao.Telefone;
            cliente.Endereco = atualizacao.Endereco;

            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();

            return Ok(cliente);
        }

        [HttpDelete("{id}")]
       // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Remover(int id)
        {
            var cliente = await _context.Clientes
                .Include(c => c.Reservas)
                .Include(c => c.Participacoes)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (cliente is null)
                return NotFound("Cliente não encontrado.");

            if (cliente.Reservas.Any())
                return BadRequest("Cliente possui reservas e não pode ser removido.");

            if (cliente.Participacoes.Any())
                return BadRequest("Cliente possui participações e não pode ser removido.");

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
