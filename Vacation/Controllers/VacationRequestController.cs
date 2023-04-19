using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Vacation.Models;

namespace Vacation.Controllers;

public class VacationRequestController : Controller
{
    private readonly VacationDbContext _context;

    public VacationRequestController(VacationDbContext context)
    {
        _context = context;
    }

    // GET: VacationRequests
    public async Task<IActionResult> Index()
    {
        int employeeId = int.Parse(User.FindFirst("EmployeeId").Value);
        var applicationDbContext = _context.VacationRequests
            .Include(v => v.Employee)
            .Where(v => v.Employee.ID == employeeId);
        return View(await applicationDbContext.ToListAsync());
    }


    // GET: VacationRequests/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var vacationRequest = await _context.VacationRequests
            .Include(v => v.Employee)
            .FirstOrDefaultAsync(m => m.ID == id);
        if (vacationRequest == null)
        {
            return NotFound();
        }

        return View(vacationRequest);
    }

    // GET: VacationRequests/Create
    public IActionResult Create()
    {
        ViewData["EmployeeID"] = new SelectList(_context.Employees, "ID", "FullName");
        return View();
    }

    // POST: VacationRequests/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,EmployeeId,StartDate,EndDate")] VacationRequest vacationRequest)
    {
        if (ModelState.IsValid)
        {
            _context.Add(vacationRequest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(vacationRequest);
    }


    // GET: VacationRequests/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var vacationRequest = await _context.VacationRequests.FindAsync(id);
        if (vacationRequest == null)
        {
            return NotFound();
        }
        ViewData["EmployeeID"] = new SelectList(_context.Employees, "ID", "FullName", vacationRequest.EmployeeID);
        return View(vacationRequest);
    }

    // POST: VacationRequests/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ID,StartDate,EndDate,EmployeeID,Status")] VacationRequest vacationRequest)
    {
        if (id != vacationRequest.ID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(vacationRequest);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VacationRequestExists(vacationRequest.ID))
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
        ViewData["EmployeeID"] = new SelectList(_context.Employees, "ID", "FullName", vacationRequest.EmployeeID);
        return View(vacationRequest);
    }

    // GET: VacationRequests/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var vacationRequest = await _context.VacationRequests
            .Include(v => v.Employee)
            .FirstOrDefaultAsync(m => m.ID == id);
        if (vacationRequest == null)
        {
            return NotFound();
        }

        return View(vacationRequest);
    }

    // POST: VacationRequests/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var vacationRequest = await _context.VacationRequests.FindAsync(id);
        _context.VacationRequests.Remove(vacationRequest);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool VacationRequestExists(int id)
    {
        return _context.VacationRequests.Any(e => e.ID == id);
    }
    [Authorize(Roles = "HR")]
    public async Task<IActionResult> Approve(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var vacationRequest = await _context.VacationRequests.FindAsync(id);
        if (vacationRequest == null)
        {
            return NotFound();
        }

        vacationRequest.Status = "Approved";
        _context.Update(vacationRequest);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
    [Authorize(Roles = "HR")]
    public async Task<IActionResult> Reject(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var vacationRequest = await _context.VacationRequests.FindAsync(id);
        if (vacationRequest == null)
        {
            return NotFound();
        }

        vacationRequest.Status = "Rejected";
        _context.Update(vacationRequest);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

}


