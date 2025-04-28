using api.Dtos;
using api.Dtos.Cliente;
using api.Dtos.Ganhos;
using api.Models;

namespace api.Mappers
{
    public static class GanhosMapper
    {

        public static GanhosDto ToGanhosDto(this Ganhos GanhoModel)
        {
            return new GanhosDto
            {
                Id = GanhoModel.Id,
                GanhoBruto = GanhoModel.GanhoBruto,
                TaxaServiço = GanhoModel.TaxaServiço,
                TaxaManutencao = GanhoModel.TaxaManutencao,
                TotalLiquido = GanhoModel.TotalLiquido
            };
        }

        public static Ganhos ToCLienteFromCreate(this CreateGanhosDto GanhosDto)
        {
            return new Ganhos
            {
              
                GanhoBruto = GanhosDto.GanhoBruto,
            

            };
        }


        public static Ganhos ToGanhosFromUpdate(this UpdateGanhosDto GanhosDto, int id)
        {
            return new Ganhos
            {
                
                GanhoBruto = GanhosDto.GanhoBruto,
              
            };
        }
    }
}
