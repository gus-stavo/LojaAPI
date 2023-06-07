using System.ComponentModel.DataAnnotations;
using LojaAPI.Domain.DTO.TelefoneCliente;


namespace LojaAPI.Domain.DTO.Cliente
{
    public class SelectCliente
    {
        public long codigoCliente { get; set; }

        public string? codigoCpf { get; set; }

        public string? codigoCnpj { get; set; }

        public string nomeCliente { get; set; } = null!;

        public string? nomeRazaoSocial { get; set; }

        public string? codigoCep { get; set; }

        public string? nomeLogradouro { get; set; }

        public int? numeroLogradouro { get; set; }

        public string? descricaoComplemento { get; set; }

        public string? nomeBairro { get; set; }

        public string? nomeCidade { get; set; }

        public string? codigoEstado { get; set; }

        public string descricaoEmail { get; set; } = null!;

        public string descricaoClassificacao { get; set; } = null!;

        public IEnumerable<SelectTelefone> telefones { get; set; } = null!;
    }
}
