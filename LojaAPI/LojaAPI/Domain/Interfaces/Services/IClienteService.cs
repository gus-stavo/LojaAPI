using LojaAPI.Domain.DTO.Cliente;
using LojaAPI.Domain.Models;

namespace LojaAPI.Domain.Interfaces.Services
{
    public interface IClienteService
    {
        Task<IEnumerable<Cliente>> GetClientes();

        Task<Cliente> GetCliente(long id);

        Task<int> InsertCliente(InsertCliente clienteInseridoDTO);

        Task UpdateCliente(long id, UpdateCliente clienteAtualizadoDTO);

        Task DeleteCliente(long id);
    }
}
