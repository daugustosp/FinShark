using api.Models;

namespace api.Interfaces
{
    public interface IEstadiasRepository
    {

      
        Task CreateAsync(List<Estadias> estadias);
        Task<IEnumerable<object>> GetAllAsync(string appUserId);
    }
}
