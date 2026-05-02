using System.ComponentModel.DataAnnotations;

namespace ExpedientesAcademicos.Models
{
    public class materia
    {
        public int materiaId { get; set; }

        [Required]
        public string NombreMateria { get; set; }

        [Required]
        public string Docente { get; set; }

    }
}
