using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiNook.Models
{
    [Table("cliente")]
    public class Cliente
    {
        [Column("id_cli")]
        public int Id { get; set; }

        [Column("nome_cli")]
        public required string Nome { get; set; }

        [Column("cpf_cli")]
        public required string Cpf { get; set; }

        [Column("email_cli")]
        public required string Email { get; set; }

        [Column("tel_cli")]
        public string? Telefone { get; set; }

        [Column("endereco_cli")]
        public string? Endereco { get; set; }

        [JsonIgnore]
        public ICollection<Reserva>? Reservas { get; set; }

        [JsonIgnore]
        public ICollection<ParticipacaoEvento>? Participacoes { get; set; }
    }
}
