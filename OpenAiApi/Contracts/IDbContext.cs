namespace OpenAiApi.Contracts
{
  public interface IDbContext
  {
    int SaveChanges();

    Task<int> SaveChangesAsync();
  }
}