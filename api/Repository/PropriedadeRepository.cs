using api.Data;
using api.Dtos.Account.Propriedade;
using api.Helpers;
using api.Interfaces;
using Dapper;
using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Data;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Collections.Generic;

namespace api.Repository
{
    public class PropriedadeRepository : IPropriedadeRepository
    {

        private readonly ApplicationDBContext _context;
        private readonly IDbConnection _dbConnection;


        public PropriedadeRepository(ApplicationDBContext context, IDbConnection dbConnection)
        {
            _context = context;
            _dbConnection = dbConnection;
        }

        public async Task<Propriedade> CreateAsync(Propriedade propriedadeDto)
        {
            await _context.Propriedades.AddAsync(propriedadeDto);
            await _context.SaveChangesAsync();
            return propriedadeDto;
        }

        public async Task<Propriedade?> DeleteAsync(int id)
        {
            var propriedadeDto = await _context.Propriedades.FirstOrDefaultAsync(x => x.Id == id);

            if (propriedadeDto == null)
            {
                return null;
            }

            _context.Propriedades.Remove(propriedadeDto);
            await _context.SaveChangesAsync();
            return propriedadeDto;
        }

        public async  Task<List<Propriedade>> GetAllAsync(String AppUserId)
        {
                     
               DynamicParameters param = new();           
            param.Add("AppUserId", AppUserId, DbType.String);
            var sql = @"SELECT p.* FROM sistema.dbo.Propriedade AS p
                        WHERE  p.AppUserId =@AppUserId";     
                      return (List<Propriedade>)await _dbConnection.QueryAsync<Propriedade>(sql, param);

        }

        public async Task<List<Propriedade>> GetAllAsync()
        {

            var sql = @"SELECT p.* FROM sistema.dbo.Propriedade AS p
                        order by data";

            return (List<Propriedade>)await _dbConnection.QueryAsync<Propriedade>(sql);
        }

        public async Task<Propriedade> GetByIdAsync(int id)
        {
            return await _context.Propriedades.FindAsync(id);
        }

      
        public async Task<Propriedade?> UpdateAsync(int id, PropriedadeDto propriedadeDto)
        {
            var existePropriedade = await _context.Propriedades.FirstOrDefaultAsync(x => x.Id == id);

            if (existePropriedade == null)
            {
                return null;
            }

            existePropriedade.DescricaoPropriedade = propriedadeDto.DescricaoPropriedade;
            existePropriedade.TipoPropriedade = propriedadeDto.TipoPropriedade;
            existePropriedade.EnderecoPropriedade = propriedadeDto.EnderecoPropriedade;
            existePropriedade.nomePropriedade = propriedadeDto.nomePropriedade;
            existePropriedade.qttQuartos = propriedadeDto.qttQuartos;
            existePropriedade.qttBanheiros = propriedadeDto.qttBanheiros;
            existePropriedade.tamanho = propriedadeDto.tamanho;
            existePropriedade.fotos = propriedadeDto.fotos;

            await _context.SaveChangesAsync();

            return existePropriedade;
        }

      
    }
}
