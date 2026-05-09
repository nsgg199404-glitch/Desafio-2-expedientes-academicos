using System.ComponentModel.DataAnnotations;

namespace ExpedientesAcademicos.Models
{
    public class Expediente
    {
      public int ExpedienteId { get; set; }

        [Required]
        public int AlumnoId { get; set; }

        public Alumno? Alumno { get; set; }

        public int MateriaId { get; set; }
        public Materia? Materia { get; set; }

        [Range(0,10)]
        public decimal NotaFinal { get; set; }

        public string? Observaciones { get; set; }  
    }
}
