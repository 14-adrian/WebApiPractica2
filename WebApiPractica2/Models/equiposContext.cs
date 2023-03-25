using Microsoft.EntityFrameworkCore;

namespace WebApiPractica2.Models
{
    public class equiposContext : DbContext
    {
        public equiposContext(DbContextOptions<equiposContext> options) : base(options)
        {

        }

        public DbSet<carreras> carreras { get; set; }
        public DbSet<equipos> equipos { get; set; }
        public DbSet<estados_equipo> estados_equipo { get; set; }
        public DbSet<estadoReserva> estadoReserva { get; set; }
        public DbSet<facultades> facultades { get; set; }
        public DbSet<marcas> marcas { get; set; }
        public DbSet<reserva> reserva { get; set; }
        public DbSet<tipoEquipo> tipoEquipo { get; set; }
        public DbSet<usuarios> usuarios { get; set; }

    }
}
