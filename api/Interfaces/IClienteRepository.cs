using api.Dtos.Cliente;
using api.Dtos.Stock;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IClienteRepository
    {
       Task<List<Cliente>> GetAllAsync(String AppUserId);
       Task<Cliente> GetByIdAsync(int id);
        Task<Cliente> CreateAsync(Cliente clienteModel);
        Task<Cliente?> UpdateAsync(int id, UpdateClienteDto clienteDto);
        Task<Cliente?> DeleteAsync(int id);
    }
        
    }
