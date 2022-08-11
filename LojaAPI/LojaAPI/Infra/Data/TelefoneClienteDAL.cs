using Dapper;
using LojaAPI.Domain.Interfaces.DAL;
using LojaAPI.Domain.Models;
using System.Data.SqlClient;

namespace LojaAPI.Infra.Data
{
    public class TelefoneClienteDAL : ITelefoneClienteDAL
    {
        private readonly IConfiguration _configuration;

        public TelefoneClienteDAL(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<TelefoneCliente>> GetTelefones(long id)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("LojaMSSQL"));

            return await connection.QueryAsync<TelefoneCliente>("select * from telefones_clientes where cd_Cliente = @cd_Cliente", new { cd_Cliente = id });
        }

        public async Task<int> InsertTelefone(TelefoneCliente telefoneCliente)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("LojaMSSQL"));

            return await connection.QuerySingleAsync<int>("insert into telefones_clientes (cd_Cliente, cd_Telefone) output inserted.cd_TelefonesClientes values (@cd_Cliente, @cd_Telefone)", telefoneCliente);
        }


        public async Task InsertTelefones(List<TelefoneCliente> telefonesCliente) 
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("LojaMSSQL"));

            await connection.ExecuteAsync("insert into telefones_clientes (cd_Cliente, cd_Telefone) values (@cd_Cliente, @cd_Telefone)", telefonesCliente);
        }

        public async Task UpdateTelefones(List<TelefoneCliente> telefonesCliente)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("LojaMSSQL"));

            await connection.ExecuteAsync($"update telefones_clientes set cd_Telefone = @cd_Telefone where cd_TelefonesClientes = @cd_TelefonesClientes", telefonesCliente);
        }

        public async Task DeleteTelefones(long id)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("LojaMSSQL"));

            await connection.ExecuteAsync("delete from telefones_clientes where cd_Cliente = @cd_Cliente", new { cd_Cliente = id });
        }

        public async Task DeleteTelefones(long id, List<long> ids)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("LojaMSSQL"));

            await connection.ExecuteAsync("delete from telefones_clientes where cd_Cliente = @cd_Cliente and cd_TelefonesClientes not in @cd_TelefonesClientes", new { cd_Cliente = id, cd_TelefonesClientes = ids});
        }
    }
}
