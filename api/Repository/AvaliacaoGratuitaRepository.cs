using api.Data;
using api.Dtos.Account.Propriedade;
using api.Dtos.Avaliacao;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace api.Repository
{
    public class AvaliacaoGratuitaRepository : IAvaliacaoGratuitaRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly IDbConnection _dbConnection;

        public AvaliacaoGratuitaRepository(ApplicationDBContext context, IDbConnection dbConnection)
        {
            _context = context;
            _dbConnection = dbConnection;

        }

        public async Task<AvaliacaoGratuita> CreateAsync(AvaliacaoGratuita avaliacaoGratuita)
        {
            await _context.AvaliacaoGratuita.AddAsync(avaliacaoGratuita);
            await _context.SaveChangesAsync();
            return avaliacaoGratuita;
        }

        public async Task<IEnumerable<avaliacaoStatus>> GetAllAsync(QueryObject query)
        {
            DynamicParameters param = new();
            var data1 = query.dataInicial;
            var data2 = query.dataFinal;
            param.Add("data1", data1, DbType.DateTime);
            param.Add("data2", data2, DbType.DateTime);           
            var sql = @" SELECT * FROM AvaliacaoGratuita
              WHERE dataCriacacao BETWEEN @data1 AND @data2
                 ORDER BY dataCriacacao DESC";


            return (List<avaliacaoStatus>)await _dbConnection.QueryAsync<avaliacaoStatus>(sql, param);
        }

        public async Task<IEnumerable<Statuscontador>> GetConsolidado(QueryObject query)
        {
            DynamicParameters param = new();
            var data1 = query.dataInicial;
            var data2 = query.dataFinal;
            param.Add("data1", data1, DbType.DateTime);
            param.Add("data2", data2, DbType.DateTime);
            var sql = @"
              SELECT status, count(status) as contador FROM sistema.dbo.AvaliacaoGratuita
              WHERE dataCriacacao BETWEEN @data1 AND @data2
             group by status";
            return (List<Statuscontador>)await _dbConnection.QueryAsync<Statuscontador>(sql, param);
        }

        public async  Task<IEnumerable<AvaliacaoDto>> GetStatus(QueryObject query)
        {
            DynamicParameters param = new();
            var id = query.id;
            param.Add("id", id, DbType.Int32);
                   var sql = @"
            SELECT * FROM AvaliacaoGratuita
            WHERE id=@id";
            return (List<AvaliacaoDto>)await _dbConnection.QueryAsync<AvaliacaoDto>(sql, param);
        }



        public async Task<AvaliacaoDto?> UpdateAsync(int id, AvaliacaoDto avaliacaoDto)
        {
            var status = avaliacaoDto.status;
            DynamicParameters param = new();
            param.Add("id", id, DbType.Int32); // ou DbType.Guid, dependendo do tipo real de 'id'
            param.Add("status", status, DbType.String); // ou outro tipo conforme necessário

            var sql = @"UPDATE sistema.dbo.AvaliacaoGratuita
            SET status = @status
            WHERE id = @id;";

            // Se você só precisa executar a query e não retornar nada:
            await _dbConnection.ExecuteAsync(sql, param);

            // Se você quiser retornar o objeto atualizado, você precisará fazer outro SELECT
            var selectSql = "SELECT * FROM sistema.dbo.AvaliacaoGratuita WHERE id = @id;";
            var avaliacaoAtualizada = await _dbConnection.QuerySingleOrDefaultAsync<AvaliacaoDto>(selectSql, param);

            return avaliacaoAtualizada;
        }

    
    }
}
