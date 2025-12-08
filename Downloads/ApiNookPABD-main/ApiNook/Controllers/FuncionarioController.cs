using ApiNook.DataContexts;
using ApiNook.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiNook.Controllers
{
    [Route("funcionarios")]
    [ApiController]
 //   [Authorize(Roles = "Admin")]
    public class FuncionarioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FuncionarioController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetFuncionarios()
        {
            return Ok(await _context.Funcionarios.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFuncionarioPorId(int id)
        {
            var funcionario = await _context.Funcionarios.FindAsync(id);
            if (funcionario is null) return NotFound();
            return Ok(funcionario);
        }

        [HttpPost]
        public async Task<IActionResult> CriarFuncionario([FromBody] Funcionario novo)
        {
            if (_context.Funcionarios.Any(f => f.Cpf == novo.Cpf))
                return BadRequest("CPF já cadastrado.");

            if (_context.Funcionarios.Any(f => f.Email == novo.Email))
                return BadRequest("Email já cadastrado.");

            await _context.Funcionarios.AddAsync(novo);
            await _context.SaveChangesAsync();
            return Created("", novo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] Funcionario dados)
        {
            var funcionario = await _context.Funcionarios.FindAsync(id);
            if (funcionario is null) return NotFound();

            if (_context.Funcionarios.Any(f => f.Email == dados.Email && f.Id != id))
                return BadRequest("Email já cadastrado.");

            funcionario.Nome = dados.Nome;
            funcionario.Cpf = dados.Cpf;
            funcionario.Email = dados.Email;
            funcionario.Telefone = dados.Telefone;
            funcionario.Funcao = dados.Funcao;

            _context.Funcionarios.Update(funcionario);
            await _context.SaveChangesAsync();
            return Ok(funcionario);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            var funcionario = await _context.Funcionarios.FindAsync(id);
            if (funcionario is null) return NotFound();

            _context.Funcionarios.Remove(funcionario);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
