using Microsoft.EntityFrameworkCore;

public class GamingAdminContext(DbContextOptions<GamingAdminContext> options) : DbContext(options)
{
    public DbSet<GamingAdmin.Models.Game> Game { get; set; } = default!;

    public DbSet<GamingAdmin.Models.Genre> Genre { get; set; } = default!;
    public DbSet<GamingAdmin.Models.GamerRating> GamerRating { get; set; } = default!;

}
