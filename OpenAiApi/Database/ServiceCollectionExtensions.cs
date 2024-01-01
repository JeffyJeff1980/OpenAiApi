using Microsoft.EntityFrameworkCore;
using OpenAiApi.Contracts;

namespace OpenAiApi.Database
{
  public static class ServiceCollectionExtensions
  {
    public static void AddSqlServerDatabase(this IServiceCollection services, string connectionString, string migrationsAssemblyName)
    {
      services.AddDbContextProvider<DatabaseContext>((provider, options) =>
      {
        options.UseSqlServer(connectionString, sqlServerOptions => sqlServerOptions.MigrationsAssembly(migrationsAssemblyName));
      });
    }

    public static void AddDbContextProvider<TContext>(
      this IServiceCollection services,
      Action<IServiceProvider, DbContextOptionsBuilder> action) where TContext : DbContext, IDbContext
    {
      services.AddScoped<IDbContext, TContext>();
      services.AddDbContext<TContext>(action);
    }
  }
}