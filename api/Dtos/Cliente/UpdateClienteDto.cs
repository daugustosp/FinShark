using System.ComponentModel.DataAnnotations;
namespace api.Dtos.Cliente
{
    public class UpdateClienteDto
    {

        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string email { get; set; }
        public string Cpf { get; set; }
      
    }
}
