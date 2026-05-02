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
        public DbSet<materia> materias { get; set; }    
        public DbSet<expediente> expedientes { get; set; }


    }
}
