using Microsoft.EntityFrameworkCore;
using uno_csharp_platform.Models;

namespace uno_csharp_platform.Data;

public class AppDbContext : DbContext
{
    public DbSet<CartItem> CartItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var dbPath = GetDatabasePath();
            optionsBuilder.UseSqlite($"Filename={dbPath}");
        }
    }

    private static string GetDatabasePath()
    {
        var fileName = "cart.db3";
        
#if __ANDROID__
        var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        return Path.Combine(path, fileName);
#elif __IOS__
        var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        var libraryPath = Path.Combine(path, "..", "Library");
        return Path.Combine(libraryPath, fileName);
#elif WINDOWS
        var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        return Path.Combine(path, fileName);
#elif __WASM__
        // For WASM, use a simple path
        return fileName;
#else
        var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        return Path.Combine(path, fileName);
#endif
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Price)
            .HasColumnType("decimal(18,2)")
            ;
        });
    }
}