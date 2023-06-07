using LojaAPI.Domain.Interfaces.DAL;
using LojaAPI.Domain.Interfaces.Services;
using LojaAPI.Domain.Models;
using LojaAPI.Domain.DTO.TelefoneCliente;
using LojaAPI.Domain.Parser.ParserTelefone;

namespace LojaAPI.Services
{
    public class TelefoneService : ITelefoneService
    {
        private readonly ITelefoneDAL _telefoneDAL;

        public TelefoneService(ITelefoneDAL telefoneDAL)
        {
            _telefoneDAL = telefoneDAL;
        }

        //public async Task<IEnumerable<SelectTelefone>> GetTelefones(long idCliente)
        //{
        //    var telefonesCliente = await _telefoneClienteDAL.GetTelefones(idCliente);

        //    List<SelectTelefone> telefonesClienteDTO = new List<SelectTelefone>();

        //    foreach (var telefoneCliente in telefonesCliente) 
        //    {
        //        var telefoneClienteDTO = new SelectTelefone()
        //        {
        //            cd_TelefonesClientes = telefoneCliente.cdTelefone,
        //            cd_Telefone = telefoneCliente.nrTelefone,
        //        };

        //        telefonesClienteDTO.Add(telefoneClienteDTO);
        //    }

        //    return telefonesClienteDTO;
        //}

        public async Task CreateTelefones(long cdCliente, IEnumerable<InsertTelefone> telefonesDTO)
        {
            IEnumerable<Telefone> telefones = await ParserInsertTelefone.Parse(cdCliente, telefonesDTO);
            await _telefoneDAL.CreateTelefones(telefones);
        }

        public async Task UpdateTelefones(long codigoCliente, IEnumerable<UpdateTelefone> telefonesDTO)
        {
            IEnumerable<Telefone> telefones = await ParserUpdateTelefone.Parse(codigoCliente, telefonesDTO);
            await _telefoneDAL.UpdateTelefones(codigoCliente, telefones);
        }

        //public async Task DeleteTelefones(long idCliente)
        //{
        //    await _telefoneClienteDAL.DeleteTelefones(idCliente);
        //}
    }
}
