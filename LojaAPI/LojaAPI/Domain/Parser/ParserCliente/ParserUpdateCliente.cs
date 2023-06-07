using LojaAPI.Domain.DTO.Cliente;
using LojaAPI.Domain.Models;
using System.Collections.Concurrent;
using LojaAPI.Domain.Parser.ParserTelefone;

namespace LojaAPI.Domain.Parser.ParserCliente
{
    public class ParserUpdateCliente
    {
        public static async Task<UpdateCliente> Parse(Cliente item)
        {
            return await Task.FromResult(new UpdateCliente()
            {
                codigoCliente = item.cdCliente,
                codigoCpf = item.cdCpf,
                codigoCnpj = item.cdCnpj,
                nomeCliente = item.nmCliente,
                nomeRazaoSocial = item.nmRazaoSocial,
                codigoCep = item.cdCep,
                numeroLogradouro = item.nrLogradouro,
                descricaoEmail = item.dsEmail,
                descricaoClassificacao = item.dsClassificacao,
            });
        }

        public static async Task<Cliente> Parse(UpdateCliente item)
        {
            return await Task.FromResult(new Cliente()
            {
                cdCliente = item.codigoCliente,
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

        public static async Task<IEnumerable<UpdateCliente>> Parse(IEnumerable<Cliente> items)
        {
            ConcurrentBag<UpdateCliente> itemsRetorno = new();
            items.ToList().ForEach(async x => itemsRetorno.Add(await Parse(x)));
            return await Task.FromResult(itemsRetorno);
        }

        public static async Task<IEnumerable<Cliente>> Parse(IEnumerable<UpdateCliente> items)
        {
            ConcurrentBag<Cliente> itemsRetorno = new();
            items.ToList().ForEach(async x => itemsRetorno.Add(await Parse(x)));
            return await Task.FromResult(itemsRetorno);
        }
    }
}
