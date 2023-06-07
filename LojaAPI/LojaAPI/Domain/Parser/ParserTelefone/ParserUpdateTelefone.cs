using LojaAPI.Domain.DTO.Cliente;
using LojaAPI.Domain.DTO.TelefoneCliente;
using LojaAPI.Domain.Models;
using System.Collections.Concurrent;

namespace LojaAPI.Domain.Parser.ParserTelefone
{
    public class ParserUpdateTelefone
    {
        public static async Task<UpdateTelefone> Parse(Telefone item)
        {
            return await Task.FromResult(new UpdateTelefone()
            {
                codigoTelefone = item.cdTelefone,
                numeroTelefone = item.nrTelefone,
            });
        }

        public static async Task<Telefone> Parse(long codigoCliente, UpdateTelefone item)
        {
            return await Task.FromResult(new Telefone()
            {
                cdTelefone = item.codigoTelefone,
                nrTelefone = item.numeroTelefone,
                cdCliente = codigoCliente,
            });
        }

        public static async Task<IEnumerable<UpdateTelefone>> Parse(IEnumerable<Telefone> items)
        {
            ConcurrentBag<UpdateTelefone> itemsRetorno = new();
            items.ToList().ForEach(async x => itemsRetorno.Add(await Parse(x)));
            return await Task.FromResult(itemsRetorno);
        }

        public static async Task<IEnumerable<Telefone>> Parse(long codigoCliente, IEnumerable<UpdateTelefone> items)
        {
            ConcurrentBag<Telefone> itemsRetorno = new();
            items.ToList().ForEach(async x => itemsRetorno.Add(await Parse(codigoCliente, x)));
            return await Task.FromResult(itemsRetorno);
        }
    }
}
