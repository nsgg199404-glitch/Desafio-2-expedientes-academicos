using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExpedientesAcademicos.Data;
using ExpedientesAcademicos.Models;
using ExpedientesAcademicos.Models.ViewModels;

namespace ExpedientesAcademicos.Controllers
{
    public class ExpedientesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExpedientesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var expedientes = _context.expedientes
                .Include(e => e.Alumno)
                .Include(e => e.Materia);

            return View(await expedientes.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var expediente = await _context.expedientes
                .Include(e => e.Alumno)
                .Include(e => e.Materia)
                .FirstOrDefaultAsync(m => m.ExpedienteId == id);

            if (expediente == null) return NotFound();

            return View(expediente);
        }

        public IActionResult Create()
        {
            CargarListas();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExpedienteId,AlumnoId,MateriaId,NotaFinal,Observaciones")] Expediente expediente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(expediente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            CargarListas(expediente.AlumnoId, expediente.MateriaId);
            return View(expediente);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var expediente = await _context.expedientes.FindAsync(id);

            if (expediente == null) return NotFound();

            CargarListas(expediente.AlumnoId, expediente.MateriaId);
            return View(expediente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExpedienteId,AlumnoId,MateriaId,NotaFinal,Observaciones")] Expediente expediente)
        {
            if (id != expediente.ExpedienteId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expediente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpedienteExists(expediente.ExpedienteId))
                        return NotFound();

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            CargarListas(expediente.AlumnoId, expediente.MateriaId);
            return View(expediente);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var expediente = await _context.expedientes
                .Include(e => e.Alumno)
                .Include(e => e.Materia)
                .FirstOrDefaultAsync(m => m.ExpedienteId == id);

            if (expediente == null) return NotFound();

            return View(expediente);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expediente = await _context.expedientes.FindAsync(id);

            if (expediente != null)
            {
                _context.expedientes.Remove(expediente);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Promedios()
        {
            var promedios = await _context.expedientes
                .Include(e => e.Alumno)
                .GroupBy(e => new { e.AlumnoId, e.Alumno.Nombre, e.Alumno.Apellido })
                .Select(g => new PromedioAlumnoViewModel
                {
                    alumnoId = g.Key.AlumnoId,
                    NombreCompleto = g.Key.Nombre + " " + g.Key.Apellido,
                    Promedio = g.Average(e => e.NotaFinal)
                })
                .ToListAsync();

            return View(promedios);
        }

        private bool ExpedienteExists(int id)
        {
            return _context.expedientes.Any(e => e.ExpedienteId == id);
        }

        private void CargarListas(int? alumnoId = null, int? materiaId = null)
        {
            ViewData["AlumnoId"] = new SelectList(
                _context.alumnos.Select(a => new
                {
                    a.AlumnoId,
                    NombreCompleto = a.Nombre + " " + a.Apellido
                }),
                "AlumnoId",
                "NombreCompleto",
                alumnoId
            );

            ViewData["MateriaId"] = new SelectList(
                _context.materias,
                "MateriaId",
                "NombreMateria",
                materiaId
            );
        }
    }
}









