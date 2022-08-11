using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaAPI.Domain.Models
{
    [Table("clientes")]
    public class Cliente
    {
        [Key]
        public long cd_Cliente { get; set; }

        [MaxLength(11)]
        public string? cd_CPF { get; set; }

        [MaxLength(14)]
        public string? cd_CNPJ { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage = "O campo \"Nome\" é obrigatório.")]
        public string nm_Cliente { get; set; } = null!;

        [MaxLength(100)]
        public string? nm_RazaoSocial { get; set; }

        [MaxLength(8)]
        public string? cd_CEP { get; set; }

        [MaxLength(100)]
        public string? nm_Logradouro { get; set; }

        public int? cd_Logradouro { get; set; }

        [MaxLength(100)]
        public string? ds_Complemento { get; set; }

        [MaxLength(100)]
        public string? nm_Bairro { get; set; }

        [MaxLength(100)]
        public string? nm_Cidade { get; set; }

        [MaxLength(2)]
        public string? cd_Estado { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage = "O campo \"E-mail\" é obrigatório.")]
        public string ds_Email { get; set; } = null!;

        [Required(ErrorMessage = "Insira ao menos um telefone.")]
        public List<TelefoneCliente> telefonesCliente { get; set; } = null!;

        [MaxLength(12)]
        [Required(ErrorMessage = "O campo \"Classificação\" é obrigatório.")]
        public string ds_Classificacao { get; set; } = null!;
    }
}
