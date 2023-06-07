using LojaAPI.Domain.DTO.TelefoneCliente;
using LojaAPI.Domain.Models;

namespace LojaAPI.Domain.Interfaces.Services
{
    public interface ITelefoneService
    {
        //Task<IEnumerable<SelectTelefone>> GetTelefones(long idCliente);

        Task CreateTelefones(long cdCliente, IEnumerable<InsertTelefone> telefonesDTO);

        Task UpdateTelefones(long codigoCliente, IEnumerable<UpdateTelefone> telefonesDTO);

        //Task DeleteTelefones(long codigoCliente, IEnumerable<UpdateTelefone> telefonesDTO);
    }
}
