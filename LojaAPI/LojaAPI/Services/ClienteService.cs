using LojaAPI.Domain.DTO.Cliente;
using LojaAPI.Domain.DTO.TelefoneCliente;
using LojaAPI.Domain.Interfaces.DAL;
using LojaAPI.Domain.Interfaces.Services;
using LojaAPI.Domain.Models;
using LojaAPI.Domain.Parser.ParserCliente;
using LojaAPI.Infra.CrossCutting;

namespace LojaAPI.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteDAL _clienteDAL;

        public ClienteService(IClienteDAL clienteDAL)
        {
            _clienteDAL = clienteDAL;
        }

        public async Task<IEnumerable<SelectCliente>> GetClientes()
        {
            IEnumerable<Cliente> clientes = await _clienteDAL.GetClientes();
            return await ParserSelectCliente.Parse(clientes);
        }

        public async Task<SelectCliente> GetClienteById(long codigoCliente)
        {
            Cliente cliente = await _clienteDAL.GetClienteById(codigoCliente);

            if (cliente is null) throw new ArgumentNullException();

            return await ParserSelectCliente.Parse(cliente);
        }

        public async Task<long> CreateCliente(InsertCliente clienteDTO)
        {
            await Validations.ValidateTelefone(clienteDTO.telefones);
            Cliente cliente = await ParserInsertCliente.Parse(clienteDTO);
            await Validations.ValidateInputs(cliente);
            return await _clienteDAL.CreateCliente(cliente);
        }

        public async Task UpdateCliente(UpdateCliente clienteDTO)
        {
            await Validations.ValidateTelefone(clienteDTO.telefones);
            Cliente cliente = await ParserUpdateCliente.Parse(clienteDTO);
            await Validations.ValidateInputs(cliente);
            await _clienteDAL.UpdateCliente(cliente, clienteDTO);
        }

        public async Task DeleteCliente(SelectCliente clienteDTO)
        {
            Cliente cliente = await ParserSelectCliente.Parse(clienteDTO);
            await _clienteDAL.DeleteCliente(cliente);
        }
    }
}
