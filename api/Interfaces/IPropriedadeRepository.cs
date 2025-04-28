
using api.Dtos.Account.Propriedade;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IPropriedadeRepository
    {
        Task<List<Propriedade>> GetAllAsync(String AppUserId);
        Task<List<Propriedade>> GetAllAsync();
        Task<Propriedade> GetByIdAsync(int id);
        Task<Propriedade> CreateAsync(Propriedade propriedadeDto);
        Task<Propriedade?> UpdateAsync(int id, PropriedadeDto propriedadeDto);
        Task<Propriedade?> DeleteAsync(int id);
    }
}
