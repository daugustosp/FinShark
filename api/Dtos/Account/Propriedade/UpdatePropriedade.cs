using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Account.Propriedade
{
    public class UpdatePropriedade
    {


        public int Id { get; set; }

        public string EnderecoPropriedade { get; set; }

        public string TipoPropriedade { get; set; }

        public string DescricaoPropriedade { get; set; }
   
        public string AppUserId { get; set; }

        public int IdCliente { get; set; }
    }
}
