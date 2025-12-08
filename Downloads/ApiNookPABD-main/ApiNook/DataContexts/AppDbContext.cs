using ApiNook.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ApiNook.DataContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<ParticipacaoEvento> Participacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Cliente)
                .WithMany(c => c.Reservas)
                .HasForeignKey(r => r.ClienteId);

            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Livro)
                .WithMany(l => l.Reservas)
                .HasForeignKey(r => r.LivroId);

            modelBuilder.Entity<ParticipacaoEvento>()
                .HasOne(p => p.Cliente)
                .WithMany(c => c.Participacoes)
                .HasForeignKey(p => p.ClienteId);

            modelBuilder.Entity<ParticipacaoEvento>()
                .HasOne(p => p.Evento)
                .WithMany(e => e.Participacoes)
                .HasForeignKey(p => p.EventoId);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                         .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
