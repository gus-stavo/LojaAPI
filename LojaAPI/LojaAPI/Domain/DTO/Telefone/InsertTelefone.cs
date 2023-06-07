using System.ComponentModel.DataAnnotations;

namespace LojaAPI.Domain.DTO.TelefoneCliente
{
    public class InsertTelefone
    {
        [MaxLength(11)]
        [Required(ErrorMessage = "O campo \"Número de Telefone\" é obrigatório.")]
        public string numeroTelefone { get; set; } = null!;
    }
}
