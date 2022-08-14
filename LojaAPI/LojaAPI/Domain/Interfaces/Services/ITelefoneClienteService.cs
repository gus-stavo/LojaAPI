using LojaAPI.Domain.DTO.TelefoneCliente;
using LojaAPI.Domain.Models;

namespace LojaAPI.Domain.Interfaces.Services
{
    public interface ITelefoneClienteService
    {
        Task<IEnumerable<SelectTelefoneCliente>> GetTelefones(long idCliente);

        Task InsertTelefones(long idCliente, List<InsertTelefoneCliente> telefonesClienteDTO);

        Task UpdateTelefones(long idCliente, List<UpdateTelefoneCliente> telefonesClienteDTO);

        Task DeleteTelefones(long idCliente);
    }
}
