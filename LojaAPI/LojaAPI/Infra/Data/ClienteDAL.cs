using Dapper;
using LojaAPI.Domain.Interfaces.DAL;
using LojaAPI.Domain.Models;
using System.Data.SqlClient;

namespace LojaAPI.Infra.Data
{
    public class ClienteDAL : IClienteDAL
    {
        private readonly IConfiguration _configuration;

        public ClienteDAL(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<Cliente>> GetClientes() 
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("LojaMSSQL"));

            var clientes = await connection.QueryAsync<Cliente>("select * from clientes");

            foreach (var cliente in clientes)
            {
                var telefonesCliente = await connection.QueryAsync<TelefoneCliente>("select * from telefones_clientes where cd_Cliente = @cd_Cliente", new { cd_Cliente = cliente.cd_Cliente });
                cliente.telefonesCliente = telefonesCliente.ToList();
            }

            return clientes;
        }

        public async Task<Cliente> GetCliente(long id) 
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("LojaMSSQL"));

            return await connection.QueryFirstOrDefaultAsync<Cliente>("select * from clientes where cd_Cliente = @cd_Cliente", new { cd_Cliente = id });
        }

        public async Task<int> InsertCliente(Cliente clienteInserido)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("LojaMSSQL"));

            return await connection.QuerySingleAsync<int>("insert into clientes (cd_CPF, cd_CNPJ, nm_Cliente, nm_RazaoSocial, cd_CEP, nm_Logradouro, cd_Logradouro, ds_Complemento, nm_Bairro, nm_Cidade, cd_Estado, ds_Email, ds_Classificacao) output inserted.cd_Cliente values (@cd_CPF, @cd_CNPJ, @nm_Cliente, @nm_RazaoSocial, @cd_CEP, @nm_Logradouro, @cd_Logradouro, @ds_Complemento, @nm_Bairro, @nm_Cidade, @cd_Estado, @ds_Email, @ds_Classificacao)", clienteInserido);
        }

        public async Task UpdateCliente(Cliente clienteAtualizado)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("LojaMSSQL"));

            await connection.ExecuteAsync("update clientes set cd_CPF = @cd_CPF, cd_CNPJ = @cd_CNPJ, nm_Cliente = @nm_Cliente, nm_RazaoSocial = @nm_RazaoSocial, cd_CEP = @cd_CEP, nm_Logradouro = @nm_Logradouro, cd_Logradouro = @cd_Logradouro, ds_Complemento = @ds_Complemento, nm_Bairro = @nm_Bairro, nm_Cidade = @nm_Cidade, cd_Estado = @cd_Estado, ds_Email = @ds_Email, ds_Classificacao = @ds_Classificacao where cd_Cliente = @cd_Cliente", clienteAtualizado);
        }

        public async Task DeleteCliente(long id)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("LojaMSSQL"));

            await connection.ExecuteAsync("delete from clientes where cd_Cliente = @cd_Cliente", new { cd_Cliente = id });
        }
    }
}
