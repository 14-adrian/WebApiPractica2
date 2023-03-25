using System.ComponentModel.DataAnnotations;

namespace WebApiPractica2.Models
{
    public class facultades
    {
        [Key]
        public int facultadId { get; set; }

        public string? nombreFacultad { get; set; }
    }
}