using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiNook.Models
{
    [Table("evento")]
    public class Evento
    {
        [Column("id_eve")]
        public int Id { get; set; }

        [Column("titulo_eve")]
        public required string Titulo { get; set; }

        [Column("descricao_eve")]
        public required string Descricao { get; set; }

        [Column("data_eve")]
        public DateTime Data { get; set; }

        [Column("vagas_eve")]
        public int Vagas { get; set; }

        [JsonIgnore]
        public ICollection<ParticipacaoEvento>? Participacoes { get; set; }
    }
}
