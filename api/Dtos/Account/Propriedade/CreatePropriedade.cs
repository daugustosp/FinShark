using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Account.Propriedade
{
    public class CreatePropriedade
    {

        [Required]
        public string EnderecoPropriedade { get; set; }
        [Required]
        public string TipoPropriedade { get; set; }

        public string DescricaoPropriedade { get; set; }
        [Required]
        public int IdCliente { get; set; }

    }
}
