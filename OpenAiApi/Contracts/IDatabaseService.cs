using OpenAiApi.Models;

namespace OpenAiApi.Contracts
{
  public interface IDatabaseService
  {
    Task<List<Customer>> GetCustomers();

    Task<List<Order>> GetOrders();

    Task<List<Product>> GetProducts();
  }
}