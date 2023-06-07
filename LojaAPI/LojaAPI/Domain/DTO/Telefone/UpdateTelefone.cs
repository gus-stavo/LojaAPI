using System.ComponentModel.DataAnnotations;

namespace LojaAPI.Domain.DTO.TelefoneCliente
{
    public class UpdateTelefone
    {
        [Required(ErrorMessage = "O campo \"Id Telefone\" é obrigatório.")]
        public long codigoTelefone { get; set; }

        [MaxLength(11)]
        [Required(ErrorMessage = "O campo \"Número de Telefone\" é obrigatório.")]
        public string numeroTelefone { get; set; } = null!;
    }
}
