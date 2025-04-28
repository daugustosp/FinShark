using api.Dtos;
using api.Dtos.Cliente;
using api.Dtos.Comment;
using api.Models;

namespace api.Mappers
{
    public static class ClienteMapper
    {
        public static ClienteDto ToClienteDto(this Cliente clienteModel)
        {
            return new ClienteDto
            {
                id = clienteModel.id,
                Nome = clienteModel.Nome,
                email = clienteModel.email,
                Endereco =  clienteModel.Endereco,
                Cpf =   clienteModel.Cpf
          
              
            };

        }

         public static Cliente ToCLienteFromCreate(this CreateClienteDto clienteDto)
        {
            return new Cliente
            {
                Nome = clienteDto.Nome,
                Endereco = clienteDto.endereco,
                email = clienteDto.email,
                Cpf = clienteDto.Cpf
           
    };
        }


        public static Cliente ToClienteFromUpdate(this UpdateClienteDto clienteDto, int id)
        {
            return new Cliente
            {
                Nome = clienteDto.Nome,
                Endereco = clienteDto.Endereco,
                email = clienteDto.email,
                Cpf = clienteDto.Cpf
            };
        }

    }
}