using System.ComponentModel.DataAnnotations;

namespace api.Dtos
{
    public class ClienteDto
    {
        public int id { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string email { get; set; }
        public string Cpf { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}