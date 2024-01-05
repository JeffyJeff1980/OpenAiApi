using Microsoft.EntityFrameworkCore;
using OpenAiApi.Contracts;
using OpenAiApi.Models;

namespace OpenAiApi.Database
{
  public class DatabaseContext : AbstractContext<DatabaseContext>, IDbContext
  {
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
  }
}