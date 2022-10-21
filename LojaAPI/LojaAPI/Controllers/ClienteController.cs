using LojaAPI.Domain.DTO.Cliente;
using LojaAPI.Domain.Exceptions;
using LojaAPI.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace LojaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        private readonly ITelefoneClienteService _telefoneClienteService;

        public ClienteController(IConfiguration configuration, IClienteService clienteService, ITelefoneClienteService telefoneClienteService)
        {
            _clienteService = clienteService;
            _telefoneClienteService = telefoneClienteService;
        }

        /// <summary>
        /// Retorna lista de clientes
        /// </summary>
        /// <response code="200">Retorna lista de clientes</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SelectCliente>>> GetClientes()
        {
            return Ok(await _clienteService.GetClientes());
        }

        /// <summary>
        /// Retorna cliente por id
        /// </summary>
        /// <response code="200">Retorna cliente</response>
        /// <response code="404">Cliente não encontrado</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<SelectCliente>> GetCliente(long id)
        {
            var cliente = await _clienteService.GetCliente(id);
            if (cliente == null) return StatusCode(404, new { message = $"Não foi encontrado nenhum cliente com o id {id}." });

            var telefonesCliente = await _telefoneClienteService.GetTelefones(id);
            cliente.telefonesCliente = telefonesCliente.ToList();

            return Ok(cliente);
        }

        /// <summary>
        /// Cria cliente
        /// </summary>
        /// <response code="201">Cliente criado</response>
        /// <response code="422">Informação inválida ou não informada</response>
        /// <response code="503">Servidor VIACEP indisponível</response>
        [HttpPost]
        public async Task<ActionResult> InsertCliente([FromBody] InsertCliente clienteInseridoDTO)
        {
            try
            {
                var id = await _clienteService.InsertCliente(clienteInseridoDTO);
                await _telefoneClienteService.InsertTelefones(id, clienteInseridoDTO.telefonesCliente);
                return CreatedAtAction(nameof(GetCliente), new { id }, id);
            }
            catch (InputValidationException e)
            {
                return StatusCode(422, new { message = e.Message });
            }
            catch (ServiceUnavailableException e) 
            {
                return StatusCode(503, new { message = e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = $"Erro no Insert Cliente: {e.Message}" });
            }
        }

        /// <summary>
        /// Atualiza cliente
        /// </summary>
        /// <response code="204">Cliente atualizado</response>
        /// <response code="404">Cliente não encontrado</response>
        /// <response code="409">Id do cliente inválido</response>
        /// <response code="422">Informação inválida ou não informada</response>
        /// <response code="503">Servidor VIACEP indisponível</response>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatetCliente(long id, [FromBody] UpdateCliente clienteAtualizadoDTO)
        {
            if (clienteAtualizadoDTO.cd_Cliente != id) return StatusCode(409, new { message = "Você está tentando atualizar o cliente errado." });

            var cliente = await _clienteService.GetCliente(id);
            if (cliente == null) return StatusCode(404, new { message = $"Não foi encontrado nenhum cliente com o id {id}." });

            try
            {
                await _clienteService.UpdateCliente(id, clienteAtualizadoDTO);
                await _telefoneClienteService.UpdateTelefones(id, clienteAtualizadoDTO.telefonesCliente);
                return NoContent();
            }
            catch (InputValidationException e)
            {
                return StatusCode(422, new { message = e.Message });
            }
            catch (ServiceUnavailableException e)
            {
                return StatusCode(503, new { message = e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = $"Erro no Update Cliente: {e.Message}" });
            }
        }

        /// <summary>
        /// Exclui cliente
        /// </summary>
        /// <response code="204">Cliente excluído</response>
        /// <response code="404">Cliente não encontrado</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCliente(long id)
        {
            var cliente = await _clienteService.GetCliente(id);
            if (cliente == null) return StatusCode(404, new { message = $"Não foi encontrado nenhum cliente com o id {id}." });

            try 
            {
                await _telefoneClienteService.DeleteTelefones(id);
                await _clienteService.DeleteCliente(id);
                return NoContent();
            }
            catch (Exception e) 
            {
                return StatusCode(500, new { message = $"Erro no Delete Cliente: {e.Message}" });
            }
        }
    }
}
