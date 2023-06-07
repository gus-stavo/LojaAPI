using LojaAPI.Domain.DTO.Cliente;
using LojaAPI.Domain.Interfaces.DAL;
using LojaAPI.Domain.Models;
using LojaAPI.Domain.Parser.ParserTelefone;
using LojaAPI.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace LojaAPI.Infra.Data
{
    public class ClienteDAL : IClienteDAL
    {
        private readonly LojaDbContext _context;

        public ClienteDAL(LojaDbContext context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<Cliente>> GetClientes() 
        {
            return await _context.Clientes.Include(c => c.telefones).ToListAsync();
        }

        public async Task<Cliente> GetClienteById(long codigoCliente) 
        {
            return await _context.Clientes.Include(c => c.telefones).Where(c => c.cdCliente == codigoCliente).FirstOrDefaultAsync();
        }

        public async Task<long> CreateCliente(Cliente cliente)
        {
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();

            return cliente.cdCliente;
        }

        public async Task UpdateCliente(Cliente cliente, UpdateCliente clienteDTO)
        {
            cliente.cdCpf = clienteDTO.codigoCpf;
            cliente.cdCnpj = clienteDTO.codigoCnpj;
            cliente.nmCliente = clienteDTO.nomeCliente;
            cliente.nmRazaoSocial = clienteDTO.nomeRazaoSocial;
            cliente.cdCep = clienteDTO.codigoCep;
            cliente.nrLogradouro = clienteDTO.numeroLogradouro;
            cliente.dsEmail = clienteDTO.descricaoEmail;
            cliente.dsClassificacao = clienteDTO.descricaoClassificacao;

            //_context.Clientes.Attach(cliente);
            //_context.Clientes.Entry(cliente).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteCliente(Cliente cliente)
        {
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
        }
    }
}
