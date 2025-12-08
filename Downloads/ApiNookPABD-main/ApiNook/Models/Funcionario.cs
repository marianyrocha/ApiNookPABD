using System.ComponentModel.DataAnnotations.Schema;

namespace ApiNook.Models
{
    [Table("funcionario")]
    public class Funcionario
    {
        [Column("id_fun")]
        public int Id { get; set; }

        [Column("nome_fun")]
        public required string Nome { get; set; }

        [Column("cpf_fun")]
        public required string Cpf { get; set; }

        [Column("email_fun")]
        public required string Email { get; set; }

        [Column("tel_fun")]
        public string? Telefone { get; set; }

        [Column("funcao_fun")]
        public required string Funcao { get; set; }
    }
}
