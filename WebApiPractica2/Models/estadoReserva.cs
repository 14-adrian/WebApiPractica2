using System.ComponentModel.DataAnnotations;

namespace WebApiPractica2.Models
{
    public class estadoReserva
    {
        [Key]
        public int estadoResId { get; set; }

        public string? estado { get; set; }
    }
}