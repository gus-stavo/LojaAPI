using System.ComponentModel.DataAnnotations;

namespace LojaAPI.Domain.DTO.TelefoneCliente
{
    public class SelectTelefoneCliente
    {
        [Key]
        public long cd_TelefonesClientes { get; set; }

        [MaxLength(11)]
        [Required]
        public string cd_Telefone { get; set; } = null!;
    }
}
