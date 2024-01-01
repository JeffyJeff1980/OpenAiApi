using Microsoft.EntityFrameworkCore;
using OpenAiApi.Contracts;
using OpenAiApi.Database;
using OpenAiApi.Models;

namespace OpenAiApi.Services
{
  public class DatabaseService : IDatabaseService
  {
    private readonly DatabaseContext _dbContext;

    public DatabaseService(DatabaseContext dbContext)
    {
      _dbContext = dbContext;
    }

    // get customers
    public async Task<List<Customer>> GetCustomers()
    {
      return await _dbContext.Customers.ToListAsync();
    }

    // get products
    public async Task<List<Product>> GetProducts()
    {
      return await _dbContext.Products.ToListAsync();
    }

    // get orders
    public async Task<List<Order>> GetOrders()
    {
      return await _dbContext.Orders.ToListAsync();
    }
  }
}