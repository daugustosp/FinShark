using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("AvaliacaoGratuita")]
    public class AvaliacaoGratuita
    {
        public int id { get; set; }
        public string nomeCompleto { get; set; }
        public string email { get; set; }
        public string telefone { get; set; }
        public string comoConheceu { get; set; }
        public string endereco { get; set; }
        public string tipoImovel { get; set; }
        public string area { get; set; }
        public string nQuartos { get; set; }
        public string nBanheiros { get; set; }
        public string vagas { get; set; }
        public string imovelImobiliado { get; set; }
        public string comodidades { get; set; }        
        public string detalhes { get; set; }
        public string fotos { get; set; }
        public bool imovelTemporada { get; set; }
        public string expectativaFaturamento { get; set; }
        public string observ { get; set; }
        public DateTime dataCriacacao { get; set; } = DateTime.Now;
     
    }
}
