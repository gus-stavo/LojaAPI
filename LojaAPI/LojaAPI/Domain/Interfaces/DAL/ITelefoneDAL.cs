using LojaAPI.Domain.Models;

namespace LojaAPI.Domain.Interfaces.DAL
{
    public interface ITelefoneDAL
    {
        //Task<IEnumerable<Telefone>> GetTelefones(long idCliente);

        //Task<int> InsertTelefone(Telefone telefoneCliente);

        Task CreateTelefones(IEnumerable<Telefone> telefones);

        Task UpdateTelefones(long codigoCliente, IEnumerable<Telefone> telefones);

        //Task DeleteTelefones(IEnumerable<Telefone> telefones);

        //Task DeleteTelefones(long idCliente, List<long> ids);
    }
}
