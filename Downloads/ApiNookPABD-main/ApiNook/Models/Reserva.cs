using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiNook.Models
{
    [Table("reserva")]
    public class Reserva
    {
        [Column("id_res")]
        public int Id { get; set; }

        [Column("id_cli_fk")]
        public int ClienteId { get; set; }

        [JsonIgnore]
        public Cliente? Cliente { get; set; }

        [Column("id_liv_fk")]
        public int LivroId { get; set; }

        [JsonIgnore]
        public Livro? Livro { get; set; }

        [Column("data_reserva")]
        public DateTime DataReserva { get; set; } = DateTime.UtcNow;

        [Column("status_reserva")]
        public string Status { get; set; } = "Pendente";
    }
}
