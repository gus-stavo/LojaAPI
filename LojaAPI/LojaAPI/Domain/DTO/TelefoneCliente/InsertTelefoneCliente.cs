using System.ComponentModel.DataAnnotations;

namespace LojaAPI.Domain.DTO.TelefoneCliente
{
    public class InsertTelefoneCliente
    {
        [MaxLength(11)]
        [Required]
        public string cd_Telefone { get; set; } = null!;
    }
}
