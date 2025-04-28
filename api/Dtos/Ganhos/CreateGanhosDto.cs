using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Ganhos
{
    public class CreateGanhosDto
    {

        [Required]
        public decimal GanhoBruto { get; set; } 
       
       
    }
}
