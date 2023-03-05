using System.Reflection;
using BuberDinner.Domain.Menu;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BuberDinner.Infrastructure.Persistence;

public class BuberDinnerDbContext : DbContext
{
    public BuberDinnerDbContext(DbContextOptions<BuberDinnerDbContext> options) : base(options)
    {
        
    }

    public DbSet<Menu> Menus { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BuberDinnerDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    private void DisableDefaultIdGeneration(ModelBuilder modelBuilder)
    {
        modelBuilder.Model.GetEntityTypes()
            .SelectMany(e => e.GetProperties())
            .Where(p => p.IsPrimaryKey())
            .ToList()
            .ForEach(p => p.ValueGenerated = ValueGenerated.Never);
    }
}

