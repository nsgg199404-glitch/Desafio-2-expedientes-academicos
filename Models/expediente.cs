using System.ComponentModel.DataAnnotations;

namespace ExpedientesAcademicos.Models
{
    public class expediente
    {
      public int expedienteId { get; set; }

        [Required]
        public int alumnoId { get; set; }

        public Alumno? Alumno { get; set; }

        public int materiaId { get; set; }
        public Materia? materia { get; set; }

        [Range(0,10)]
        public decimal NotaFinal { get; set; }

        public string? Observaciones { get; set; }  
    }
}
