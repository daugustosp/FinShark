using api.Data;
using api.Dtos;
using api.Dtos.Ganhos;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class GanhosRepository : IGanhosRepository
    {
        private readonly ApplicationDBContext _context;

        public GanhosRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Ganhos> CreateAsync(Ganhos ganhos)
        {
            await _context.Ganho.AddAsync(ganhos);
            await _context.SaveChangesAsync();
            return ganhos;
        }

        public async Task<Ganhos?> DeleteAsync(int id)
        {
            var GanhoModel = await _context.Ganho.FirstOrDefaultAsync(x => x.Id == id);

            if (GanhoModel == null)
            {
                return null;
            }

            _context.Ganho.Remove(GanhoModel);
            await _context.SaveChangesAsync();
            return GanhoModel;
        }

        public async Task<List<Ganhos>> GetAllAsync(QueryObject query)
        {
            return await _context.Ganho
               .FromSqlRaw("SELECT * FROM Ganhos")
                .ToListAsync();
        }

         async Task<Ganhos> IGanhosRepository.GetByIdAsync(int id)
        {
            return await _context.Ganho.FindAsync(id);
        }

        public async Task<Ganhos?> UpdateAsync(int id, UpdateGanhosDto ganhosDto)
        {
            var existingGanho = await _context.Ganho.FirstOrDefaultAsync(x => x.Id == id);

            if (existingGanho == null)
            {
                return null;
            }

            existingGanho.GanhoBruto = ganhosDto.GanhoBruto;
            existingGanho.TaxaManutencao = ganhosDto.TaxaManutencao;
            existingGanho.TaxaServiço = ganhosDto.TaxaServiço;
            existingGanho.TotalLiquido = ganhosDto.TotalLiquido;

            await _context.SaveChangesAsync();

            return existingGanho;
        }
    }
}
