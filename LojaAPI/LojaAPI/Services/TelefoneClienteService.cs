using LojaAPI.Domain.Interfaces.DAL;
using LojaAPI.Domain.Interfaces.Services;
using LojaAPI.Domain.Models;

namespace LojaAPI.Services
{
    public class TelefoneClienteService : ITelefoneClienteService
    {
        private readonly ITelefoneClienteDAL _telefoneClienteDAL;

        public TelefoneClienteService(ITelefoneClienteDAL telefoneClienteDAL)
        {
            _telefoneClienteDAL = telefoneClienteDAL;
        }

        public async Task<IEnumerable<TelefoneCliente>> GetTelefones(long id)
        {
            return await _telefoneClienteDAL.GetTelefones(id);
        }

        public async Task InsertTelefones(long id, List<TelefoneCliente> telefonesCliente)
        {
            telefonesCliente.ForEach(tc => { tc.cd_Cliente = id; });

            await _telefoneClienteDAL.InsertTelefones(telefonesCliente);
        }

        public async Task UpdateTelefones(long id, List<TelefoneCliente> telefonesCliente)
        {
            List<TelefoneCliente> telefonesClienteAtualizados = new List<TelefoneCliente>();
            List<TelefoneCliente> telefonesClientesinseridos = new List<TelefoneCliente>();
            List<long> cd_TelefonesClientes = new List<long>();

            foreach (var telefoneCliente in telefonesCliente)
            {
                if (telefoneCliente.cd_TelefonesClientes > 0) telefonesClienteAtualizados.Add(telefoneCliente);
                if (telefoneCliente.cd_TelefonesClientes == 0) telefonesClientesinseridos.Add(telefoneCliente);
            }

            await _telefoneClienteDAL.UpdateTelefones(telefonesClienteAtualizados);
            telefonesClienteAtualizados.ForEach(tca => { cd_TelefonesClientes.Add(tca.cd_TelefonesClientes); });

            telefonesClientesinseridos.ForEach(tci => { tci.cd_Cliente = id; });

            foreach (var telefoneClienteInserido in telefonesClientesinseridos)
            {
                telefoneClienteInserido.cd_Cliente = id;
                cd_TelefonesClientes.Add(await _telefoneClienteDAL.InsertTelefone(telefoneClienteInserido));
            }

            await _telefoneClienteDAL.DeleteTelefones(id, cd_TelefonesClientes);
        }

        public async Task DeleteTelefones(long id)
        {
            await _telefoneClienteDAL.DeleteTelefones(id);
        }
    }
}
