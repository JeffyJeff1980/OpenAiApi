using Microsoft.EntityFrameworkCore;

namespace OpenAiApi.Database
{
  public abstract class AbstractContext<TContext> : DbContext where TContext : DbContext
  {
    public AbstractContext(DbContextOptions<TContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges()
    {
      return base.SaveChanges();
    }

    public Task<int> SaveChangesAsync()
    {
      return base.SaveChangesAsync();
    }

    public override void Dispose()
    {
      base.Dispose();
    }

    public override ValueTask DisposeAsync()
    {
      return base.DisposeAsync();
    }
  }
}