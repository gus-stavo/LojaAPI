using LojaAPI.Domain.DTO.Cliente;
using LojaAPI.Domain.DTO.TelefoneCliente;
using LojaAPI.Domain.Models;
using LojaAPI.Infra.Data;
using System.Collections.Concurrent;

namespace LojaAPI.Domain.Parser.ParserTelefone
{
    public class ParserInsertTelefone
    {
        public static async Task<Telefone> Parse(long cdCliente, InsertTelefone item)
        {
            return await Task.FromResult(new Telefone()
            {
                nrTelefone = item.numeroTelefone,
                cdCliente = cdCliente,
            });
        }

        public static async Task<IEnumerable<Telefone>> Parse(long cdCliente, IEnumerable<InsertTelefone> items)
        {
            ConcurrentBag<Telefone> itemsRetorno = new();
            items.ToList().ForEach(async x => itemsRetorno.Add(await Parse(cdCliente, x)));
            return await Task.FromResult(itemsRetorno);
        }
    }
}
