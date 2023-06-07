using LojaAPI.Domain.DTO.Cliente;
using LojaAPI.Domain.DTO.TelefoneCliente;
using LojaAPI.Domain.Models;
using System.Collections.Concurrent;

namespace LojaAPI.Domain.Parser.ParserTelefone
{
    public class ParserSelectTelefone
    {
        public static async Task<SelectTelefone> Parse(Telefone item)
        {
            return await Task.FromResult(new SelectTelefone()
            {
                codigoTelefone = item.cdTelefone,
                numeroTelefone = item.nrTelefone,
            });
        }

        public static async Task<Telefone> Parse(long codigoCliente, SelectTelefone item)
        {
            return await Task.FromResult(new Telefone()
            {
                cdTelefone = item.codigoTelefone,
                nrTelefone = item.numeroTelefone,
                cdCliente = codigoCliente,
            });
        }

        public static async Task<IEnumerable<SelectTelefone>> Parse(IEnumerable<Telefone> items)
        {
            ConcurrentBag<SelectTelefone> itemsRetorno = new();
            items.ToList().ForEach(async x => itemsRetorno.Add(await Parse(x)));
            return await Task.FromResult(itemsRetorno);
        }

        public static async Task<IEnumerable<Telefone>> Parse(long codigoCliente, IEnumerable<SelectTelefone> items)
        {
            ConcurrentBag<Telefone> itemsRetorno = new();
            items.ToList().ForEach(async x => itemsRetorno.Add(await Parse(codigoCliente, x)));
            return await Task.FromResult(itemsRetorno);
        }
    }
}
