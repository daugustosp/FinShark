using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("Ganhos")]
    public class Ganhos
    {
        
        public int Id { get; set; }
        public decimal GanhoBruto { get; set; }
        public decimal TaxaServiço { get; set; }
        public DateTime data { get; set; } = DateTime.Now;
        public decimal TaxaManutencao { get; set; }
        public decimal TotalLiquido { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

    }
}
