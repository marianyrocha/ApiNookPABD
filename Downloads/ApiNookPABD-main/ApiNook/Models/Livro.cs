using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiNook.Models
{
    [Table("livro")]
    public class Livro
    {
        [Column("id_liv")]
        public int Id { get; set; }

        [Column("titulo_liv")]
        public required string Titulo { get; set; }

        [Column("autor_liv")]
        public required string Autor { get; set; }

        [Column("ano_liv")]
        public int Ano { get; set; }

        [Column("quantidade_liv")]
        public int Quantidade { get; set; }

        [JsonIgnore]
        public ICollection<Reserva>? Reservas { get; set; }
    }
}
