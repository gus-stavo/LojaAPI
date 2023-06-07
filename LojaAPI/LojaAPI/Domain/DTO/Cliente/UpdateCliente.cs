using System.ComponentModel.DataAnnotations;
using LojaAPI.Domain.DTO.TelefoneCliente;

namespace LojaAPI.Domain.DTO.Cliente
{
    public class UpdateCliente
    {
        [Required(ErrorMessage = "O campo \"Id Cliente\" é obrigatório.")]
        public long codigoCliente { get; set; }

        [MaxLength(11)]
        public string? codigoCpf { get; set; }

        [MaxLength(14)]
        public string? codigoCnpj { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage = "O campo \"Nome\" é obrigatório.")]
        public string nomeCliente { get; set; } = null!;

        [MaxLength(100)]
        public string? nomeRazaoSocial { get; set; }

        [MaxLength(8)]
        public string? codigoCep { get; set; }

        public int? numeroLogradouro { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage = "O campo \"E-mail\" é obrigatório.")]
        public string descricaoEmail { get; set; } = null!;

        [MaxLength(12)]
        [Required(ErrorMessage = "O campo \"Classificação\" é obrigatório.")]
        public string descricaoClassificacao { get; set; } = null!;

        [Required(ErrorMessage = "Insira ao menos um telefone.")]
        public IEnumerable<UpdateTelefone> telefones { get; set; } = null!;
    }
}
