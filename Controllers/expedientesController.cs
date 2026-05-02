using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExpedientesAcademicos.Data;
using ExpedientesAcademicos.Models;

namespace ExpedientesAcademicos.Controllers
{
    public class expedientesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public expedientesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: expedientes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.expedientes.Include(e => e.Alumno).Include(e => e.materia);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: expedientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expediente = await _context.expedientes
                .Include(e => e.Alumno)
                .Include(e => e.materia)
                .FirstOrDefaultAsync(m => m.expedienteId == id);
            if (expediente == null)
            {
                return NotFound();
            }

            return View(expediente);
        }

        // GET: expedientes/Create
        public IActionResult Create()
        {
            ViewData["alumnoId"] = new SelectList(_context.alumnos, "AlumnoId", "Apellido");
            ViewData["materiaId"] = new SelectList(_context.materias, "materiaId", "Docente");
            return View();
        }

        // POST: expedientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("expedienteId,alumnoId,materiaId,NotaFinal,Observaciones")] expediente expediente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(expediente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["alumnoId"] = new SelectList(_context.alumnos, "AlumnoId", "Apellido", expediente.alumnoId);
            ViewData["materiaId"] = new SelectList(_context.materias, "materiaId", "Docente", expediente.materiaId);
            return View(expediente);
        }

        // GET: expedientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expediente = await _context.expedientes.FindAsync(id);
            if (expediente == null)
            {
                return NotFound();
            }
            ViewData["alumnoId"] = new SelectList(_context.alumnos, "AlumnoId", "Apellido", expediente.alumnoId);
            ViewData["materiaId"] = new SelectList(_context.materias, "materiaId", "Docente", expediente.materiaId);
            return View(expediente);
        }

        // POST: expedientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("expedienteId,alumnoId,materiaId,NotaFinal,Observaciones")] expediente expediente)
        {
            if (id != expediente.expedienteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expediente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!expedienteExists(expediente.expedienteId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["alumnoId"] = new SelectList(_context.alumnos, "AlumnoId", "Apellido", expediente.alumnoId);
            ViewData["materiaId"] = new SelectList(_context.materias, "materiaId", "Docente", expediente.materiaId);
            return View(expediente);
        }

        // GET: expedientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expediente = await _context.expedientes
                .Include(e => e.Alumno)
                .Include(e => e.materia)
                .FirstOrDefaultAsync(m => m.expedienteId == id);
            if (expediente == null)
            {
                return NotFound();
            }

            return View(expediente);
        }

        // POST: expedientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expediente = await _context.expedientes.FindAsync(id);
            if (expediente != null)
            {
                _context.expedientes.Remove(expediente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool expedienteExists(int id)
        {
            return _context.expedientes.Any(e => e.expedienteId == id);
        }
    }
}
