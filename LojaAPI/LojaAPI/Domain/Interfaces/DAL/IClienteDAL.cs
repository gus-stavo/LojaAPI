using LojaAPI.Domain.Models;

namespace LojaAPI.Domain.Interfaces.DAL
{
    public interface IClienteDAL
    {
        Task<IEnumerable<Cliente>> GetClientes();

        Task<Cliente> GetCliente(long id);

        Task<int> InsertCliente(Cliente clienteInserido);

        Task UpdateCliente(Cliente clienteAtualizado);

        Task DeleteCliente(long id);
    }
}
