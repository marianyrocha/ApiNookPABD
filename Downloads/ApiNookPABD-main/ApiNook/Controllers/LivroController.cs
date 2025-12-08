using ApiNook.DataContexts;
using ApiNook.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiNook.Controllers
{
    [Route("livros")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LivroController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
       // [Authorize]
        public async Task<IActionResult> GetLivros([FromQuery] string? titulo, [FromQuery] string? autor)
        {
            var query = _context.Livros.AsQueryable();

            if (!string.IsNullOrEmpty(titulo))
                query = query.Where(l => l.Titulo.Contains(titulo));

            if (!string.IsNullOrEmpty(autor))
                query = query.Where(l => l.Autor.Contains(autor));

            return Ok(await query.ToListAsync());
        }

        [HttpGet("{id}")]
    //    [Authorize]
        public async Task<IActionResult> GetLivroPorId(int id)
        {
            var livro = await _context.Livros
                .Include(l => l.Reservas)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (livro is null) return NotFound();
            return Ok(livro);
        }

        [HttpPost]
     //   [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Criar([FromBody] Livro novo)
        {
            await _context.Livros.AddAsync(novo);
            await _context.SaveChangesAsync();
            return Created("", novo);
        }

        [HttpPut("{id}")]
      //  [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] Livro dados)
        {
            var livro = await _context.Livros.FindAsync(id);
            if (livro is null) return NotFound();

            livro.Titulo = dados.Titulo;
            livro.Autor = dados.Autor;
            livro.Ano = dados.Ano;
            livro.Quantidade = dados.Quantidade;

            _context.Livros.Update(livro);
            await _context.SaveChangesAsync();
            return Ok(livro);
        }

        [HttpDelete("{id}")]
      //  [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Remover(int id)
        {
            var livro = await _context.Livros
                .Include(l => l.Reservas)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (livro is null) return NotFound();

            if (livro.Reservas.Any())
                return BadRequest("Este livro possui reservas e não pode ser removido.");

            _context.Livros.Remove(livro);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
