using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos;
using api.Dtos.Cliente;
using api.Dtos.Stock;
using api.Helpers;
using api.Interfaces;
using api.Migrations;
using api.Models;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly IDbConnection _dbConnection;

        public ClienteRepository(ApplicationDBContext context, IDbConnection dbConnection)
        {
            _context = context;
            _dbConnection = dbConnection;

        }

        public async Task<Cliente> CreateAsync(Cliente cliente)
        {
            await _context.Cliente.AddAsync(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }


        public async Task<List<Cliente>> GetAllAsync(String AppUserId)
        {

            DynamicParameters param = new();
            param.Add("AppUserId", AppUserId, DbType.String);
            var sql = @"SELECT * FROM clientes WHERE AppUserId =@AppUserId";

            return (List<Cliente>)await _dbConnection.QueryAsync<Cliente>(sql, param);

        }

        async Task<Cliente> IClienteRepository.GetByIdAsync(int id)
        {
            return await _context.Cliente.FindAsync(id);
        }


        public async Task<Cliente?> UpdateAsync(int id, UpdateClienteDto clienteDto)
        {
            var existingcliente = await _context.Cliente.FirstOrDefaultAsync(x => x.id == id);

            if (existingcliente == null)
            {
                return null;
            }

            existingcliente.Cpf = clienteDto.Cpf;
            existingcliente.email = clienteDto.email;
            existingcliente.Endereco = clienteDto.Endereco;
            existingcliente.Nome = clienteDto.Nome;

            await _context.SaveChangesAsync();

            return existingcliente;
        }
        public async Task<Cliente?> DeleteAsync(int id)
        {
            var clienteModel = await _context.Cliente.FirstOrDefaultAsync(x => x.id == id);

            if (clienteModel == null)
            {
                return null;
            }

            _context.Cliente.Remove(clienteModel);
            await _context.SaveChangesAsync();
            return clienteModel;
        }


    }
}