using System.ComponentModel.DataAnnotations;

namespace WebApiPractica2.Models
{
    public class reserva
    {
        [Key]
        public int reservaId { get; set; }

        public int equipoId { get; set; }

        public int usuarioId { get; set; }

        public  string? fechaSalida { get; set; }

        public string? horaSalida { get; set; }

        public int tiempoReserva { get; set; }

        public int estadoResId { get; set; }

        public string? fechaRetorno { get; set; }

        public string? horaRetorno { get; set; }
    }
}