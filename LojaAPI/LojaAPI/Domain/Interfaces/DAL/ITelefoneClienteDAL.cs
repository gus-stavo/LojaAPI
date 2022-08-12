using LojaAPI.Domain.Models;

namespace LojaAPI.Domain.Interfaces.DAL
{
    public interface ITelefoneClienteDAL
    {
        Task<IEnumerable<TelefoneCliente>> GetTelefones(long idCliente);

        Task<int> InsertTelefone(TelefoneCliente telefoneCliente);

        Task InsertTelefones(List<TelefoneCliente> telefonesCliente);

        Task UpdateTelefones(List<TelefoneCliente> telefonesCliente);

        Task DeleteTelefones(long idCliente);

        Task DeleteTelefones(long idCliente, List<long> ids);

    }
}
