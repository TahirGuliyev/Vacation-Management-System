using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vacation.Models;

namespace Vacation.Controllers
{
    public class VacationDaysController : Controller
    {
        private readonly VacationDbContext _context;

        public VacationDaysController(VacationDbContext context)
        {
            _context = context;
        }

        // GET: VacationDays
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.VacationDays.Include(v => v.Job);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: VacationDays/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacationDays = await _context.VacationDays
                .Include(v => v.Job)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (vacationDays == null)
            {
                return NotFound();
            }

            return View(vacationDays);
        }

        // GET: VacationDays/Create
        public IActionResult Create()
        {
            ViewData["JobID"] = new SelectList(_context.Jobs, "ID", "Name");
            return View();
        }

        // POST: VacationDays/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,DaysCount,Note,JobID")] VacationDays vacationDays)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vacationDays);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["JobID"] = new SelectList(_context.Jobs, "ID", "Name", vacationDays.JobID);
            return View(vacationDays);
        }

        // GET: VacationDays/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacationDays = await _context.VacationDays.FindAsync(id);
            if (vacationDays == null)
            {
                return NotFound();
            }
            ViewData["JobID"] = new SelectList(_context.Jobs, "ID", "Name", vacationDays.JobID);
            return View(vacationDays);
        }

        // POST: VacationDays/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,DaysCount,Note,JobID")] VacationDays vacationDays)
        {
            if (id != vacationDays.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vacationDays);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VacationDaysExists(vacationDays.ID))
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
            ViewData["JobID"] = new SelectList(_context.Jobs, "ID", "Name", vacationDays.JobID);
            return View(vacationDays);
        }

        // GET: VacationDays/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacationDays = await _context.VacationDays
                .Include(v => v.Job)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (vacationDays == null)
            {
                return NotFound();
            }

            return View(vacationDays);
        }

        // POST: VacationDays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vacationDays = await _context.VacationDays.FindAsync(id);
            _context.VacationDays.Remove(vacationDays);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VacationDaysExists(int id)
        {
            return _context.VacationDays.Any(e => e.ID == id);
        }
    }
}

    
