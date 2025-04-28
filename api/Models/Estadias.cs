using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.PortableExecutable;

namespace api.Models
{
    [Table("estadias")]
    public class Estadias
    {
        public int Id { get; set; }
        public string Codigodeconfirmacao { get; set; }
        public string Status { get; set; }
        public string Nomehospede { get; set; }
        public string EntrarEmcontato { get; set; }
        public string NumeroAdultos { get; set; }
        public string NumeroCriancas { get; set; }
        public string NumeroBb { get; set; }
        public string DataInicio { get; set; }
        public string DataTermino { get; set; }
        public string NumeroNoites { get; set; }
        public string Reservado { get; set; }
        public string Anuncio { get; set; }
        public string Ganhos { get; set; }
        public int idCliente { get; set; }
        public string AppUserId { get; set; }
    
    }
}
