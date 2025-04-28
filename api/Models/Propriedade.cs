using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("Propriedade")]
    public class Propriedade
    {
        public int Id { get; set; }
        public string EnderecoPropriedade { get; set; }
        public string TipoPropriedade { get; set; }
        public string DescricaoPropriedade { get; set; }
        public DateTime Data { get; set; } = DateTime.Now;
        public string AppUserId { get; set; }
        public int IdCliente { get; set; }
        public string nomePropriedade { get; set; }
        public int qttQuartos { get; set; }
        public int qttBanheiros { get; set; }
        public int tamanho { get; set; }
        public string fotos { get; set; }
    }
}
