
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GamingAdmin.Models;

public class GamerRatingsController : Controller
{
    private readonly GamingAdminContext _context;

    public GamerRatingsController(GamingAdminContext context)
    {
        _context = context;
    }

    // GET: GAMERRATINGS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.GamerRating.ToListAsync());
    }

    // GET: GAMERRATINGS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var gamerrating = await _context.GamerRating
            .FirstOrDefaultAsync(m => m.Id == id);
        if (gamerrating == null)
        {
            return NotFound();
        }

        return View(gamerrating);
    }

    // GET: GAMERRATINGS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: GAMERRATINGS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Games,Title")] GamerRating gamerrating)
    {
        if (ModelState.IsValid)
        {
            _context.Add(gamerrating);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(gamerrating);
    }

    // GET: GAMERRATINGS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var gamerrating = await _context.GamerRating.FindAsync(id);
        if (gamerrating == null)
        {
            return NotFound();
        }
        return View(gamerrating);
    }

    // POST: GAMERRATINGS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Games,Title")] GamerRating gamerrating)
    {
        if (id != gamerrating.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(gamerrating);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GamerRatingExists(gamerrating.Id))
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
        return View(gamerrating);
    }

    // GET: GAMERRATINGS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var gamerrating = await _context.GamerRating
            .FirstOrDefaultAsync(m => m.Id == id);
        if (gamerrating == null)
        {
            return NotFound();
        }

        return View(gamerrating);
    }

    // POST: GAMERRATINGS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var gamerrating = await _context.GamerRating.FindAsync(id);
        if (gamerrating != null)
        {
            _context.GamerRating.Remove(gamerrating);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool GamerRatingExists(int? id)
    {
        return _context.GamerRating.Any(e => e.Id == id);
    }
}
