using System.Reflection;
using MongoDB.Driver;

namespace Discount.API.Repositories;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Features.Discounts.Discount> Discounts { get; set; } = null!;
    public static AppDbContext Create(IMongoDatabase database) {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>().UseMongoDB(database.Client,database.DatabaseNamespace.DatabaseName);
        var appDbContext = new AppDbContext(optionsBuilder.Options);
        return new AppDbContext(optionsBuilder.Options);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}