using LojaAPI.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace LojaAPI.Domain.DTO.Cliente
{
    public class InsertCliente
    {
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

        public int? cd_Logradouro { get; set; }

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
