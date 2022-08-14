using LojaAPI.Domain.Interfaces.DAL;
using LojaAPI.Domain.Interfaces.Services;
using LojaAPI.Domain.Models;
using LojaAPI.Domain.DTO.TelefoneCliente;

namespace LojaAPI.Services
{
    public class TelefoneClienteService : ITelefoneClienteService
    {
        private readonly ITelefoneClienteDAL _telefoneClienteDAL;

        public TelefoneClienteService(ITelefoneClienteDAL telefoneClienteDAL)
        {
            _telefoneClienteDAL = telefoneClienteDAL;
        }

        public async Task<IEnumerable<SelectTelefoneCliente>> GetTelefones(long idCliente)
        {
            var telefonesCliente = await _telefoneClienteDAL.GetTelefones(idCliente);

            List<SelectTelefoneCliente> telefonesClienteDTO = new List<SelectTelefoneCliente>();

            foreach (var telefoneCliente in telefonesCliente) 
            {
                var telefoneClienteDTO = new SelectTelefoneCliente()
                {
                    cd_TelefonesClientes = telefoneCliente.cd_TelefonesClientes,
                    cd_Telefone = telefoneCliente.cd_Telefone,
                };

                telefonesClienteDTO.Add(telefoneClienteDTO);
            }

            return telefonesClienteDTO;
        }

        public async Task InsertTelefones(long idCliente, List<InsertTelefoneCliente> telefonesClienteDTO)
        {
            List<TelefoneCliente> telefonesCliente = new List<TelefoneCliente>();

            foreach (var telefoneClienteDTO in telefonesClienteDTO) 
            {
                var telefoneCliente = new TelefoneCliente()
                {
                    cd_Cliente = idCliente,
                    cd_Telefone = telefoneClienteDTO.cd_Telefone,
                };

                telefonesCliente.Add(telefoneCliente);
            }

            await _telefoneClienteDAL.InsertTelefones(telefonesCliente);
        }

        public async Task UpdateTelefones(long idCliente, List<UpdateTelefoneCliente> telefonesClienteDTO)
        {
            List<TelefoneCliente> telefonesCliente = new List<TelefoneCliente>();

            foreach (var telefoneClienteDTO in telefonesClienteDTO)
            {
                var telefoneCliente = new TelefoneCliente()
                {
                    cd_TelefonesClientes = telefoneClienteDTO.cd_TelefonesClientes,
                    cd_Cliente = idCliente,
                    cd_Telefone = telefoneClienteDTO.cd_Telefone,
                };

                telefonesCliente.Add(telefoneCliente);
            }

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

            //telefonesClientesinseridos.ForEach(tci => { tci.cd_Cliente = idCliente; });

            foreach (var telefoneClienteInserido in telefonesClientesinseridos)
            {
                telefoneClienteInserido.cd_Cliente = idCliente;
                cd_TelefonesClientes.Add(await _telefoneClienteDAL.InsertTelefone(telefoneClienteInserido));
            }

            await _telefoneClienteDAL.DeleteTelefones(idCliente, cd_TelefonesClientes);
        }

        public async Task DeleteTelefones(long idCliente)
        {
            await _telefoneClienteDAL.DeleteTelefones(idCliente);
        }
    }
}
