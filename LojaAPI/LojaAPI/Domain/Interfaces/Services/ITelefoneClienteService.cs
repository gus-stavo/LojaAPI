using LojaAPI.Domain.Models;

namespace LojaAPI.Domain.Interfaces.Services
{
    public interface ITelefoneClienteService
    {
        Task<IEnumerable<TelefoneCliente>> GetTelefones(long id);

        Task InsertTelefones(long id, List<TelefoneCliente> telefonesCliente);

        Task UpdateTelefones(long id, List<TelefoneCliente> telefonesCliente);

        Task DeleteTelefones(long id);
    }
}
