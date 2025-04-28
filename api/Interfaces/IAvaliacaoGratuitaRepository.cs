using api.Dtos.Account.Propriedade;
using api.Dtos.Avaliacao;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IAvaliacaoGratuitaRepository
    {
        Task<AvaliacaoGratuita> CreateAsync(AvaliacaoGratuita avaliacaoGratuita);
        Task<IEnumerable<avaliacaoStatus>> GetAllAsync(QueryObject query);
        Task<IEnumerable<Statuscontador>> GetConsolidado(QueryObject query);
        Task<IEnumerable<AvaliacaoDto>> GetStatus(QueryObject query);
        Task<AvaliacaoDto?> UpdateAsync(int id, AvaliacaoDto avaliacaoDto);
    }
}
