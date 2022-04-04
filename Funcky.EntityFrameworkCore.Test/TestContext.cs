using Microsoft.EntityFrameworkCore;

namespace Funcky.EntityFrameworkCore.Test;

internal sealed class TestContext : DbContext
{
    public DbSet<Person> People { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: $"test-{Guid.NewGuid()}");
    }
}
