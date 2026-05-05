using System.ComponentModel.DataAnnotations;

namespace ExpedientesAcademicos.Models
{
    public class Materia
    {
        public int MateriaId { get; set; }

        [Required]
        public string NombreMateria { get; set; }

        [Required]
        public string Docente { get; set; }

    }
}
