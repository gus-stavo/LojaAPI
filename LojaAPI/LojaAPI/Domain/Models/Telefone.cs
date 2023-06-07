using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaAPI.Domain.Models
{
    [Table("TBL_TELEFONE")]
    public class Telefone
    {
        public long cdTelefone { get; set; }

        public string nrTelefone { get; set; } = null!;

        public long cdCliente { get; set; }

        public virtual Cliente cliente { get; set; } = null!;
    }
}
