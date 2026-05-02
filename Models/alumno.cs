using System.ComponentModel.DataAnnotations;

namespace ExpedientesAcademicos.Models
{
    public class Alumno
    {
        public int AlumnoId { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellido { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        public string Grado { get; set; }

        public ICollection<expediente>? Expedientes { get; set; }
    }
}
