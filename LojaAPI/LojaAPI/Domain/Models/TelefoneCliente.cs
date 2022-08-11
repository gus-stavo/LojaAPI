using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaAPI.Domain.Models
{
    [Table("telefones_clientes")]
    public class TelefoneCliente
    {
        [Key]
        public long cd_TelefonesClientes { get; set; }

        [Required]
        public long cd_Cliente { get; set; }

        [Required]
        public string cd_Telefone { get; set; } = null!;
    }
}
