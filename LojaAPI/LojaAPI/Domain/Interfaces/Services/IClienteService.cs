using LojaAPI.Domain.DTO.Cliente;
using LojaAPI.Domain.Models;

namespace LojaAPI.Domain.Interfaces.Services
{
    public interface IClienteService
    {
        Task<IEnumerable<SelectCliente>> GetClientes();

        Task<SelectCliente> GetClienteById(long codigoCliente);

        Task<long> CreateCliente(InsertCliente clienteDTO);

        Task UpdateCliente(UpdateCliente clienteDTO);

        Task DeleteCliente(SelectCliente clienteDTO);
    }
}
