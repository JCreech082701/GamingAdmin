using GamingAdmin.Models;
using GamingAdmin.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

public class GamesController : Controller
{
    private readonly GamingAdminContext _context;
    private readonly IAzureStorageService _blobService;

    public GamesController(
        GamingAdminContext context,
        IAzureStorageService blobService)
    {
        _context = context;
        _blobService = blobService;
    }

    // GET: Games
    public async Task<IActionResult> Index()
    {
        return View(await _context.Game
            .Include(g => g.Genre)
            .Include(g => g.GamerRating)
            .OrderBy(g => g.ReleaseDate)
            .ToListAsync());
    }

    // GET: Games/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var game = await _context.Game
            .Include(g => g.Genre)
            .Include(g => g.GamerRating)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (game == null)
        {
            return NotFound();
        }

        return View(game);
    }

    // GET: Games/Create
    public IActionResult Create()
    {      
        ViewBag.GenreId = new SelectList(
            _context.Genre.ToList(),
            "Id",
            "Title");

        ViewBag.GamerRatingId = new SelectList(
            _context.GamerRating.ToList(),
            "Id",
            "Title");

        return View();
    }

    // POST: Games/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("Id,Title,Synopsis,GenreId,Platform,GamerRatingId,Achievements,ReleaseDate,Price,ImageUrl")]
        Game game,
        IFormFile ImageFile)
    {
        if (ImageFile == null)
        {
            ModelState.AddModelError(
                "ImageFile",
                "The image file upload is required");
        }

        if (ModelState.IsValid)
        {
            var extension = Path.GetExtension(ImageFile!.FileName);
            var uniqueFileName = $"{Guid.NewGuid()}{extension}";

            string imageUrl = await _blobService.UploadFileAsync(ImageFile, ImageFile.FileName);

            game.ImageUrl = imageUrl;

            _context.Add(game);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        var genres = _context.Genre.ToList();
        ViewBag.GenreId = new SelectList( genres, "Id", "Title", game.GenreId);

        var ratings = _context.GamerRating.ToList();
        ViewBag.GamerRatingId = new SelectList(ratings, "Id", "Title", game.GamerRatingId);


        return View(game);
    }

    // GET: Games/Edit/5
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

        ViewBag.GenreId = new SelectList(
            _context.Genre.ToList(),
            "Id",
            "Title",
            game.GenreId);

        ViewBag.GamerRatingId = new SelectList(
            _context.GamerRating.ToList(),
            "Id",
            "Title",
            game.GamerRatingId);

        return View(game);
    }

    // POST: Games/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int? id,
        [Bind("ImageUrl,Id,Title,Synopsis,GenreId,Platform,GamerRatingId,Achievements,ReleaseDate,Price")]
        Game game)
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

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        ViewBag.GenreId = new SelectList(
            _context.Genre.ToList(),
            "Id",
            "Title",
            game.GenreId);

        ViewBag.GamerRatingId = new SelectList(
            _context.GamerRating.ToList(),
            "Id",
            "Title",
            game.GamerRatingId);

        return View(game);
    }

    // GET: Games/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var game = await _context.Game
            .Include(g => g.Genre)
            .Include(g => g.GamerRating)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (game == null)
        {
            return NotFound();
        }

        return View(game);
    }

    // POST: Games/Delete/5
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
