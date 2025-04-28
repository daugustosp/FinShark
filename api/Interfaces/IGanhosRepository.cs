using api.Dtos.Cliente;
using api.Dtos.Ganhos;
using api.Helpers;
using api.Models;
using System.Threading.Tasks;

namespace api.Interfaces
{
    public interface IGanhosRepository
    {

        Task<List<Ganhos>> GetAllAsync(QueryObject query);
        Task<Ganhos> GetByIdAsync(int id);
        Task<Ganhos> CreateAsync(Ganhos ganhosModel);
        Task<Ganhos?> UpdateAsync(int id, UpdateGanhosDto ganhosDto);
        Task<Ganhos?> DeleteAsync(int id);
    }
}
