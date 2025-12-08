using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiNook.Models
{
    [Table("usuario")]
    public class Usuario
    {
        [Column("id_usu")]
        public int Id { get; set; }

        [Column("nome_usu")]
        public required string Nome { get; set; }

        [Column("email_usu")]
        public required string Email { get; set; }

        [JsonIgnore]
        [Column("senha_usu")]
        public required string Senha { get; set; }

        [Column("perfil_usu")]
        public required string Perfil { get; set; }
    }
}
