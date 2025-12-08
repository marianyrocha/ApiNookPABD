namespace ApiNook.Models.Dtos
{
    public class ReservaDto
    {
        public int ClienteId { get; set; }
        public int LivroId { get; set; }
        public string Status { get; set; } = "Ativa";
    }
}
