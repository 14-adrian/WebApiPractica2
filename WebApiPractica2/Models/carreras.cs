using System.ComponentModel.DataAnnotations;

namespace WebApiPractica2.Models
{
    public class carreras
    {
        [Key]
        public int carreraId { get; set; }

        public string? nombreCarrera { get; set; }

        public int facultadId { get; set; }
    }
}
