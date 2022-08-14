using LojaAPI.Domain.DTO.Cliente;
using LojaAPI.Domain.DTO.TelefoneCliente;
using LojaAPI.Domain.Interfaces.DAL;
using LojaAPI.Domain.Interfaces.Services;
using LojaAPI.Domain.Models;
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
            var clientes =  await _clienteDAL.GetClientes();

            List<SelectCliente> clientesDTO = new List<SelectCliente>();

            foreach (var cliente in clientes) 
            {
                var telefonesCliente = cliente.telefonesCliente;

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

                var clienteDTO = new SelectCliente()
                {
                    cd_Cliente = cliente.cd_Cliente,
                    cd_CPF = cliente.cd_CPF,
                    cd_CNPJ = cliente.cd_CNPJ,
                    nm_Cliente = cliente.nm_Cliente,
                    nm_RazaoSocial = cliente.nm_RazaoSocial,
                    cd_CEP = cliente.cd_CEP,
                    nm_Logradouro = cliente.nm_Logradouro,
                    cd_Logradouro = cliente.cd_Logradouro,
                    ds_Complemento = cliente.ds_Complemento,
                    nm_Bairro = cliente.nm_Bairro,
                    nm_Cidade = cliente.nm_Cidade,
                    cd_Estado = cliente.cd_Estado,
                    ds_Email = cliente.ds_Email, 
                    telefonesCliente = telefonesClienteDTO,
                    ds_Classificacao = cliente.ds_Classificacao,
                };

                clientesDTO.Add(clienteDTO);
            }

            return clientesDTO;
        }

        public async Task<SelectCliente> GetCliente(long id)
        {
            var cliente = await _clienteDAL.GetCliente(id);

            if (cliente == null) return null;
            else 
            {
                var clienteDTO = new SelectCliente()
                {
                    cd_Cliente = cliente.cd_Cliente,
                    cd_CPF = cliente.cd_CPF,
                    cd_CNPJ = cliente.cd_CNPJ,
                    nm_Cliente = cliente.nm_Cliente,
                    nm_RazaoSocial = cliente.nm_RazaoSocial,
                    cd_CEP = cliente.cd_CEP,
                    nm_Logradouro = cliente.nm_Logradouro,
                    cd_Logradouro = cliente.cd_Logradouro,
                    ds_Complemento = cliente.ds_Complemento,
                    nm_Bairro = cliente.nm_Bairro,
                    nm_Cidade = cliente.nm_Cidade,
                    cd_Estado = cliente.cd_Estado,
                    ds_Email = cliente.ds_Email,
                    ds_Classificacao = cliente.ds_Classificacao,
                };

                return clienteDTO;
            }
        }

        public async Task<int> InsertCliente(InsertCliente clienteInseridoDTO)
        {
            List<TelefoneCliente> telefonesCliente = new List<TelefoneCliente>();

            foreach (var telefoneClienteDTO in clienteInseridoDTO.telefonesCliente)
            {
                var telefoneCliente = new TelefoneCliente()
                {
                    cd_Telefone = telefoneClienteDTO.cd_Telefone,
                };

                telefonesCliente.Add(telefoneCliente);
            }

            var clienteInserido = new Cliente()
            {
                cd_CPF = clienteInseridoDTO.cd_CPF,
                cd_CNPJ = clienteInseridoDTO.cd_CNPJ,
                nm_Cliente = clienteInseridoDTO.nm_Cliente,
                nm_RazaoSocial = clienteInseridoDTO.nm_RazaoSocial,
                cd_CEP = clienteInseridoDTO.cd_CEP,
                cd_Logradouro = clienteInseridoDTO.cd_Logradouro,
                ds_Email = clienteInseridoDTO.ds_Email,
                telefonesCliente = telefonesCliente,
                ds_Classificacao = clienteInseridoDTO.ds_Classificacao,
            };

            await Validations.ValidateInputs(clienteInserido);

            return await _clienteDAL.InsertCliente(clienteInserido);
        }

        public async Task UpdateCliente(long id, UpdateCliente clienteAtualizadoDTO)
        {
            List<TelefoneCliente> telefonesCliente = new List<TelefoneCliente>();

            foreach (var telefoneClienteDTO in clienteAtualizadoDTO.telefonesCliente)
            {
                var telefoneCliente = new TelefoneCliente()
                {
                    cd_Cliente = id,
                    cd_Telefone = telefoneClienteDTO.cd_Telefone,
                };

                telefonesCliente.Add(telefoneCliente);
            }

            var clienteAtualizado = new Cliente()
            {
                cd_Cliente = clienteAtualizadoDTO.cd_Cliente,
                cd_CPF = clienteAtualizadoDTO.cd_CPF,
                cd_CNPJ = clienteAtualizadoDTO.cd_CNPJ,
                nm_Cliente = clienteAtualizadoDTO.nm_Cliente,
                nm_RazaoSocial = clienteAtualizadoDTO.nm_RazaoSocial,
                cd_CEP = clienteAtualizadoDTO.cd_CEP,
                cd_Logradouro = clienteAtualizadoDTO.cd_Logradouro,
                ds_Email = clienteAtualizadoDTO.ds_Email,
                telefonesCliente = telefonesCliente,
                ds_Classificacao = clienteAtualizadoDTO.ds_Classificacao,
            };

            await Validations.ValidateInputs(clienteAtualizado);

            await _clienteDAL.UpdateCliente(clienteAtualizado);
        }

        public async Task DeleteCliente(long id) 
        {
            await _clienteDAL.DeleteCliente(id);
        }
    }
}
