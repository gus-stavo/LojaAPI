using LojaAPI.Domain.DTO.Cliente;
using LojaAPI.Domain.Exceptions;
using LojaAPI.Domain.Interfaces.Services;
using LojaAPI.Domain.Models;
using LojaAPI.Infra.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LojaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        private readonly ITelefoneService _telefoneService;

        public ClienteController(IClienteService clienteService, ITelefoneService telefoneService)
        {
            _clienteService = clienteService;
            _telefoneService = telefoneService;
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
        /// Retorna cliente por codigoCliente
        /// </summary>
        /// <response code="200">Retorna cliente</response>
        /// <response code="404">Cliente não encontrado</response>
        [HttpGet("{codigoCliente}")]
        public async Task<ActionResult<SelectCliente>> GetClienteById(long codigoCliente)
        {
            try
            {
                return Ok(await _clienteService.GetClienteById(codigoCliente));
            }
            catch (Exception)
            {
                return StatusCode(404, new { message = $"Não foi encontrado nenhum cliente com o código {codigoCliente}." }); ;
            }
        }

        /// <summary>
        /// Cria cliente
        /// </summary>
        /// <response code="201">Cliente criado</response>
        /// <response code="422">Informação inválida ou não informada</response>
        /// <response code="503">Servidor VIACEP indisponível</response>
        [HttpPost]
        public async Task<IActionResult> CreateCliente([FromBody] InsertCliente clienteDTO)
        {
            try
            {
                long cdCliente = await _clienteService.CreateCliente(clienteDTO);
                await _telefoneService.CreateTelefones(cdCliente, clienteDTO.telefones);
                return CreatedAtAction(nameof(GetClienteById), new { codigoCliente = cdCliente }, cdCliente);
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
        /// <response code="409"´>Código do cliente inválido</response>
        /// <response code="422">Informação inválida ou não informada</response>
        /// <response code="503">Servidor VIACEP indisponível</response>
        [HttpPut("{códigoCliente}")]
        public async Task<ActionResult> UpdateCliente(long codigoCliente, [FromBody] UpdateCliente clienteDTO)
        {
            if (clienteDTO.codigoCliente != codigoCliente) return StatusCode(409, new { message = "Você está tentando atualizar o cliente errado." });

            SelectCliente cliente = await _clienteService.GetClienteById(codigoCliente);
            if (cliente is null) return StatusCode(404, new { message = $"Não foi encontrado nenhum cliente com o código {codigoCliente}." });

            try
            {
                await _clienteService.UpdateCliente(clienteDTO);
                await _telefoneService.UpdateTelefones(codigoCliente, clienteDTO.telefones);
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
        [HttpDelete("{codigoCliente}")]
        public async Task<IActionResult> DeleteCliente(long codigoCliente)
        {
            SelectCliente clienteDTO = await _clienteService.GetClienteById(codigoCliente);
            if (clienteDTO is null) return StatusCode(404, new { message = $"Não foi encontrado nenhum cliente com o código {codigoCliente}." });

            try
            {
                await _clienteService.DeleteCliente(clienteDTO);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = $"Erro no Delete Cliente: {e.Message}" });
            }
        }
    }
}
