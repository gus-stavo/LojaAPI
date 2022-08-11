using LojaAPI.Domain.Models;

namespace LojaAPI.Domain.Interfaces.DAL
{
    public interface ITelefoneClienteDAL
    {
        Task<IEnumerable<TelefoneCliente>> GetTelefones(long id);

        Task<int> InsertTelefone(TelefoneCliente telefoneCliente);

        Task InsertTelefones(List<TelefoneCliente> telefonesCliente);

        Task UpdateTelefones(List<TelefoneCliente> telefonesCliente);

        Task DeleteTelefones(long id);

        Task DeleteTelefones(long id, List<long> ids);

    }
}
