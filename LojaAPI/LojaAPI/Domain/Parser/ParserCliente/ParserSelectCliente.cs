using LojaAPI.Domain.DTO.Cliente;
using LojaAPI.Domain.Models;
using System.Collections.Concurrent;
using LojaAPI.Domain.Parser.ParserTelefone;

namespace LojaAPI.Domain.Parser.ParserCliente
{
    public class ParserSelectCliente
    {
        public static async Task<SelectCliente> Parse(Cliente item)
        {
            return await Task.FromResult(new SelectCliente()
            {
                codigoCliente = item.cdCliente,
                codigoCpf = item.cdCpf,
                codigoCnpj = item.cdCnpj,
                nomeCliente = item.nmCliente,
                nomeRazaoSocial = item.nmRazaoSocial,
                codigoCep = item.cdCep,
                nomeLogradouro = item.nmBairro,
                numeroLogradouro = item.nrLogradouro,
                descricaoComplemento = item.dsComplemento,
                nomeBairro = item.nmBairro,
                nomeCidade = item.nmCidade,
                codigoEstado = item.cdEstado,
                descricaoEmail = item.dsEmail,
                telefones = await ParserSelectTelefone.Parse(item.telefones),
                descricaoClassificacao = item.dsClassificacao,
            });
        }

        public static async Task<Cliente> Parse(SelectCliente item)
        {
            return await Task.FromResult(new Cliente()
            {
                cdCliente = item.codigoCliente,
                cdCpf = item.codigoCpf,
                cdCnpj = item.codigoCnpj,
                nmCliente = item.nomeCliente,
                nmRazaoSocial = item.nomeRazaoSocial,
                cdCep = item.codigoCep,
                nmLogradouro = item.nomeLogradouro,
                nrLogradouro = item.numeroLogradouro,
                dsComplemento = item.descricaoComplemento,
                nmBairro = item.nomeBairro,
                nmCidade = item.nomeCidade,
                cdEstado = item.codigoEstado,
                dsEmail = item.descricaoEmail,
                telefones = await ParserSelectTelefone.Parse(item.codigoCliente, item.telefones),
                dsClassificacao = item.descricaoClassificacao,
            });
        }

        public static async Task<IEnumerable<SelectCliente>> Parse(IEnumerable<Cliente> items)
        {
            ConcurrentBag<SelectCliente> itemsRetorno = new();
            items.ToList().ForEach(async x => itemsRetorno.Add(await Parse(x)));
            return await Task.FromResult(itemsRetorno);
        }

        public static async Task<IEnumerable<Cliente>> Parse(IEnumerable<SelectCliente> items)
        {
            ConcurrentBag<Cliente> itemsRetorno = new();
            items.ToList().ForEach(async x => itemsRetorno.Add(await Parse(x)));
            return await Task.FromResult(itemsRetorno);
        }
    }
}
