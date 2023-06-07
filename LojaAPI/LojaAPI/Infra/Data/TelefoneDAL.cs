using LojaAPI.Domain.Interfaces.DAL;
using LojaAPI.Domain.Models;
using LojaAPI.Infra.Context;
using System.Data.SqlClient;

namespace LojaAPI.Infra.Data
{
    public class TelefoneDAL : ITelefoneDAL
    {
        private readonly LojaDbContext _context;

        public TelefoneDAL(LojaDbContext context)
        {
            _context = context;
        }

        //public async Task<IEnumerable<Telefone>> GetTelefones(long idCliente)
        //{
        //    //return await connection.QueryAsync<Telefone>("select * from telefones_clientes where cd_Cliente = @cd_Cliente", new { cd_Cliente = idCliente });
        //}

        //public async Task<int> InsertTelefone(Telefone telefoneCliente)
        //{
        //    //return await connection.QuerySingleAsync<int>("insert into telefones_clientes (cd_Cliente, cd_Telefone) output inserted.cd_TelefonesClientes values (@cd_Cliente, @cd_Telefone)", telefoneCliente);
        //}

        public async Task CreateTelefones(IEnumerable<Telefone> telefones)
        {
            await _context.Telefones.AddRangeAsync(telefones);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTelefones(long codigoCliente, IEnumerable<Telefone> telefones)
        {
            IQueryable<Telefone> telefonesAtuais = _context.Telefones.Where(telefone => telefone.cdCliente  == codigoCliente);
            _context.Telefones.RemoveRange(telefonesAtuais);
            await _context.SaveChangesAsync();

            await _context.Telefones.AddRangeAsync(telefones);
            await _context.SaveChangesAsync();
        }

        //public async Task DeleteTelefones(long idCliente)
        //{
        //    //await connection.ExecuteAsync("delete from telefones_clientes where cd_Cliente = @cd_Cliente", new { cd_Cliente = idCliente });
        //}

        //public async Task DeleteTelefones(long idCliente, List<long> ids)
        //{
        //    //await connection.ExecuteAsync("delete from telefones_clientes where cd_Cliente = @cd_Cliente and cd_TelefonesClientes not in @cd_TelefonesClientes", new { cd_Cliente = idCliente, cd_TelefonesClientes = ids});
        //}
    }
}
