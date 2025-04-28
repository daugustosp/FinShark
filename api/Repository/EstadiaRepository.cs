using api.Data;
using api.Interfaces;
using api.Models;
using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace api.Repository
{
    public class EstadiaRepository : IEstadiasRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly IDbConnection _dbConnection;

        public EstadiaRepository(ApplicationDBContext context, IDbConnection dbConnection)
        {
            _context = context;
            _dbConnection = dbConnection;
        }

        public async Task CreateAsync(List<Estadias> estadias)
        {
           
            {
                string sql = @"
            INSERT INTO sistema.dbo.estadias
            (Codigodeconfirmacao, Status, Nomehospede, EntrarEmcontato, NumeroAdultos, NumeroCriancas, NumeroBb, DataInicio, DataTermino, NumeroNoites, Reservado, Anuncio, Ganhos, AppUserId, idCliente)
            VALUES (@Codigodeconfirmacao, @Status, @Nomehospede, @EntrarEmcontato, @NumeroAdultos, @NumeroCriancas, @NumeroBb, @DataInicio, @DataTermino, @NumeroNoites, @Reservado, @Anuncio, @Ganhos,@AppUserId, @idCliente);";

                foreach (var estadia in estadias)
                {
                    var param = new DynamicParameters();
                    param.Add("@Codigodeconfirmacao", estadia.Codigodeconfirmacao);
                    param.Add("@Status", estadia.Status);
                    param.Add("@Nomehospede", estadia.Nomehospede);
                    param.Add("@EntrarEmcontato", estadia.EntrarEmcontato);
                    param.Add("@NumeroAdultos", estadia.NumeroAdultos);
                    param.Add("@NumeroCriancas", estadia.NumeroCriancas);
                    param.Add("@NumeroBb", estadia.NumeroBb);
                    param.Add("@DataInicio", estadia.DataInicio);
                    param.Add("@DataTermino", estadia.DataTermino);
                    param.Add("@NumeroNoites", estadia.NumeroNoites);
                    param.Add("@Reservado", estadia.Reservado);
                    param.Add("@Anuncio", estadia.Anuncio);
                    param.Add("@Ganhos", estadia.Ganhos);
                    param.Add("@AppUserId", estadia.AppUserId);
                    param.Add("@idCliente", estadia.idCliente);

                    await _dbConnection.ExecuteAsync(sql, param);
                }
            }
        }

        public async Task<IEnumerable<object>> GetAllAsync(string appUserId)
        {
            DynamicParameters param = new();
            param.Add("AppUserId", appUserId, DbType.String);
            var sql = @"SELECT * FROM sistema.dbo.estadias WHERE AppUserId =@AppUserId";

            return (List<Estadias>)await _dbConnection.QueryAsync<Estadias>(sql, param);
        }
    }
}
