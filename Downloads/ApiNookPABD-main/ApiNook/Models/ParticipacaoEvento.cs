using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiNook.Models
{
    [Table("participacao_evento")]
    public class ParticipacaoEvento
    {
        [Column("id_par")]
        public int Id { get; set; }

        [Column("cliente_id")]  
        public int ClienteId { get; set; }

        [JsonIgnore]
        public Cliente? Cliente { get; set; }

        [Column("evento_id")]   
        public int EventoId { get; set; }

        [JsonIgnore]
        public Evento? Evento { get; set; }

        [Column("data_registro")]
        public DateTime DataRegistro { get; set; } = DateTime.UtcNow;
    }
}
