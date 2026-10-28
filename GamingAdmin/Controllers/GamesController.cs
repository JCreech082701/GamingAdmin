using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GamingAdmin.Models;
using GamingAdmin.Services;

public class GamesController : Controller
{
    private readonly GamingAdminContext _context;
    private readonly IAzureStorageService _blobService;

    public GamesController(GamingAdminContext context, AzureStorageService blobService)
    {
        _context = context;
        _blobService = blobService;
    }

    // GET: GAMES
    // I changed this so that the Index page displays by release date
    public async Task<IActionResult> Index()
    {
        return View(await _context.Game.OrderBy(g => g.ReleaseDate).ToListAsync());
    }

    // GET: GAMES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var game = await _context.Game
            .FirstOrDefaultAsync(m => m.Id == id);
        if (game == null)
        {
            return NotFound();
        }

        return View(game);
    }

    // GET: GAMES/Create
    public IActionResult Create()
    {
        return View();
    }

    /*
     TO-DO Handle image upload and save the image path to the database. You can use IFormFile to handle file uploads in ASP.NET Core. 
     */

    // POST: GAMES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Title,Synopsis,Genre,Platform,Rating,Achievements,ReleaseDate,Price")] Game game, IFormFile imageFile)
    {

        if (imageFile == null)
        {
            ModelState.AddModelError("ImageFile", "The image file upload is required");
        }


        if (ModelState.IsValid)
        {
        
                // Upload the image to Azure Blob Storage
            string imageUrl = await _blobService.UploadFileAsync(imageFile, imageFile.FileName);
            game.ImageUrl = imageUrl;
            _context.Add(game);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(game);
    }

    // GET: GAMES/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var game = await _context.Game.FindAsync(id);
        if (game == null)
        {
            return NotFound();
        }
        return View(game);
    }

    // POST: GAMES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("ImageUrl,Id,Title,Synopsis,Genre,Platform,Rating,Achievements,ReleaseDate,Price")] Game game)
    {
        if (id != game.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(game);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(game.Id))
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
        return View(game);
    }

    // GET: GAMES/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var game = await _context.Game
            .FirstOrDefaultAsync(m => m.Id == id);
        if (game == null)
        {
            return NotFound();
        }

        return View(game);
    }

    // POST: GAMES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var game = await _context.Game.FindAsync(id);
        if (game != null)
        {
            _context.Game.Remove(game);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool GameExists(int? id)
    {
        return _context.Game.Any(e => e.Id == id);
    }
}
