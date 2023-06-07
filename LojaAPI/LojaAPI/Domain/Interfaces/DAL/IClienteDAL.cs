using LojaAPI.Domain.DTO.Cliente;
using LojaAPI.Domain.Models;

namespace LojaAPI.Domain.Interfaces.DAL
{
    public interface IClienteDAL
    {
        Task<IEnumerable<Cliente>> GetClientes();

        Task<Cliente> GetClienteById(long codigoCliente);

        Task<long> CreateCliente(Cliente cliente);

        Task UpdateCliente(Cliente cliente, UpdateCliente clienteDTO);

        Task DeleteCliente(Cliente cliente);
    }
}
