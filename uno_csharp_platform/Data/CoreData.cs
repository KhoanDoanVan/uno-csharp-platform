using Microsoft.EntityFrameworkCore;
using uno_csharp_platform.Models;

namespace uno_csharp_platform.Data;


public class AppDbContext: DbContext
{
    
    public DbSet<CartItem> CartItems { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    {}


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if(!optionsBuilder.IsConfigured)
        {
            var dbPath = GetDatabasePath();
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.CartItemId);
            entity.Property(e => e.ProductName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(10,2)");
            entity.Property(e => e.Unit).HasMaxLength(20);
        });
    }


    private static string GetDatabasePath()
    {
        string path;

        #if __ANDROID__
            path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        #elif __IOS__
            path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            path = Path.Combine(path, "..", "Library");
        #else
            path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        #endif

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        return Path.Combine(path, "storeapp.db3");
    }


    public async Task InitializeDatabaseAsync()
    {
        await Database.EnsureCreatedAsync();
    }

}