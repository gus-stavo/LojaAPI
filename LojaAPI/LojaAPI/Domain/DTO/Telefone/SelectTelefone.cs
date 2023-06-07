using System.ComponentModel.DataAnnotations;

namespace LojaAPI.Domain.DTO.TelefoneCliente
{
    public class SelectTelefone
    {
        public long codigoTelefone { get; set; }

        public string numeroTelefone { get; set; } = null!;
    }
}
