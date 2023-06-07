using LojaAPI.Domain.DTO.Cliente;
using LojaAPI.Domain.Models;
using LojaAPI.Domain.Parser.ParserTelefone;
using LojaAPI.Infra.Data;
using System.Collections.Concurrent;

namespace LojaAPI.Domain.Parser.ParserCliente
{
    public class ParserInsertCliente
    {
        public static async Task<Cliente> Parse(InsertCliente item)
        {
            return await Task.FromResult(new Cliente()
            {
                cdCpf = item.codigoCpf,
                cdCnpj = item.codigoCnpj,
                nmCliente = item.nomeCliente,
                nmRazaoSocial = item.nomeRazaoSocial,
                cdCep = item.codigoCep,
                nrLogradouro = item.numeroLogradouro,
                dsEmail = item.descricaoEmail,
                dsClassificacao = item.descricaoClassificacao,
            });
        }

        public static async Task<IEnumerable<Cliente>> Parse(IEnumerable<InsertCliente> items)
        {
            ConcurrentBag<Cliente> itemsRetorno = new();
            items.ToList().ForEach(async x => itemsRetorno.Add(await Parse(x)));
            return await Task.FromResult(itemsRetorno);
        }
    }
}
