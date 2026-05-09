using ExpedientesAcademicos.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpedientesAcademicos.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Alumno> alumnos { get; set; }
        public DbSet<Materia> materias { get; set; }    
        public DbSet<Expediente> expedientes { get; set; }


    }
}
