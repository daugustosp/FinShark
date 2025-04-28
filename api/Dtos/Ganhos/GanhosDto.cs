namespace api.Dtos.Ganhos
{
    public class GanhosDto
    {
        public int Id { get; set; }
        public decimal GanhoBruto { get; set; }
        public decimal TaxaServiço { get; set; }
        public DateTime data { get; set; } = DateTime.Now;
        public decimal TaxaManutencao { get; set; }
        public decimal TotalLiquido { get; set; }
    }
}
