using api.Dtos;
using api.Dtos.Account.Propriedade;
using api.Dtos.Cliente;
using api.Models;

namespace api.Mappers
{
    public static class PropriedadeMApper
    {
        public static PropriedadeDto ToPropiedadeDto(this Propriedade propriedade)
        {
            return new PropriedadeDto
            {
             
                IdCliente = propriedade.IdCliente,
                DescricaoPropriedade = propriedade.DescricaoPropriedade,
                TipoPropriedade = propriedade.TipoPropriedade,
                EnderecoPropriedade = propriedade.EnderecoPropriedade,
                nomePropriedade = propriedade.nomePropriedade,
                qttQuartos = propriedade.qttQuartos,
                qttBanheiros = propriedade.qttBanheiros,
                tamanho = propriedade.tamanho,
                fotos = propriedade.fotos



            };


        }

        public static Propriedade ToPropriedadeFromCreate(this PropriedadeDto propriedade)
        {
            return new Propriedade
            {
            
                IdCliente = propriedade.IdCliente,
                DescricaoPropriedade = propriedade.DescricaoPropriedade,
                TipoPropriedade = propriedade.TipoPropriedade,
                EnderecoPropriedade = propriedade.EnderecoPropriedade,
                nomePropriedade = propriedade.nomePropriedade,
                qttQuartos = propriedade.qttQuartos,
                qttBanheiros = propriedade.qttBanheiros,
                tamanho = propriedade.tamanho,
                fotos = propriedade.fotos


            };
        }

        public static Propriedade ToPropriedadeFFromUpdate(this UpdatePropriedade propriedade, int id)
        {
            return new Propriedade
            {
                IdCliente = propriedade.IdCliente,
                DescricaoPropriedade = propriedade.DescricaoPropriedade,
                EnderecoPropriedade =  propriedade.EnderecoPropriedade,
                TipoPropriedade = propriedade.TipoPropriedade

            };
        }
    }
}
