using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Cliente
{
    public class CreateClienteDto
    {
        [Required]        
        public string Nome { get; set; } = string.Empty;

        [Required]
         public string Cpf { get; set; } = string.Empty;

        [Required]
        public string endereco { get; set; } = string.Empty;

        [Required]
        public string email { get; set; } = string.Empty;

      


    }
}