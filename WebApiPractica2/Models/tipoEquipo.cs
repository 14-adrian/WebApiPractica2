using System.ComponentModel.DataAnnotations;

namespace WebApiPractica2.Models
{
    public class tipoEquipo
    {
        [Key]
        public int tipoEquipoId { get; set; }

        public string? descripcion { get; set; }

        public string? estado { get; set; }
    }
}