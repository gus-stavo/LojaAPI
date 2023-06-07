using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaAPI.Domain.Models
{
    [Table("TBL_CLIENTE")]
    public class Cliente
    {
        public long cdCliente { get; set; }

        public string? cdCpf { get; set; }

        public string? cdCnpj { get; set; }

        public string nmCliente { get; set; } = null!;

        public string? nmRazaoSocial { get; set; }

        public string? cdCep { get; set; }

        public string? nmLogradouro { get; set; }

        public int? nrLogradouro { get; set; }

        public string? dsComplemento { get; set; }

        public string? nmBairro { get; set; }

        public string? nmCidade { get; set; }

        public string? cdEstado { get; set; }

        public string dsEmail { get; set; } = null!;

        public string dsClassificacao { get; set; } = null!;

        public virtual IEnumerable<Telefone> telefones { get; set; } = null!;
    }
}
