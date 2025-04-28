using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Ganhos
{
    public class UpdateGanhosDto
    {
        [Required]
        public decimal GanhoBruto { get; set; }
        [Required]
        public decimal TaxaServiço { get; set; }
        [Required]
        public decimal TaxaManutencao { get; set; }
        [Required]
        public decimal TotalLiquido { get; set; }
    }
}
