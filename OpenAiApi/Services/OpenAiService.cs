using Newtonsoft.Json;
using OpenAiApi.Contracts;
using System.Text;

namespace OpenAiApi.Services
{
  public class OpenAiService : IOpenAiService
  {
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly IConfiguration _configuration;
    private readonly IDatabaseService _databaseService;

    public OpenAiService(HttpClient httpClient, IConfiguration configuration, IDatabaseService databaseService)
    {
      _httpClient = httpClient;
      _configuration = configuration;
      _databaseService = databaseService;
      _apiKey = _configuration.GetSection("OpenAI:ApiKey").Value;
    }

    public async Task<string> AskOpenAI(string userPrompt, string systemPrompt)
    {
      var content = new
      {
        model = "gpt-4-1106-preview",
        messages = new List<object> {
          new { role = "system", content = systemPrompt },
          new { role = "user", content = userPrompt}
        }
      };

      var request = new HttpRequestMessage
      {
        Method = HttpMethod.Post,
        RequestUri = new Uri("https://api.openai.com/v1/chat/completions"),
        Headers = {
                { "Authorization", $"Bearer {_apiKey}" }
            },
        Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json")
      };

      var response = await _httpClient.SendAsync(request);

      return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> ConvertDataToPrompt()
    {
      StringBuilder promptBuilder = new StringBuilder();

      // Fetch data from the database
      var customers = await _databaseService.GetCustomers();
      var products = await _databaseService.GetProducts();
      var orders = await _databaseService.GetOrders();

      promptBuilder.Append("You answer questions asked by employees about the customers, the orders and the products.");
      promptBuilder.AppendLine();
      promptBuilder.AppendLine();
      promptBuilder.AppendLine("Here is a list of customers:");

      // list customers
      foreach (var customer in customers)
      {
        promptBuilder.AppendLine($"Customer ID: {customer.CustomerId} - First name: {customer.FirstName} - Last name: {customer.LastName} - City: {customer.City} - Phone: {customer.Phone}");
      }

      promptBuilder.AppendLine();
      promptBuilder.AppendLine("Here is a list of products:");

      // list products
      foreach (var product in products)
      {
        promptBuilder.AppendLine($"Product ID: {product.ProductId} - Name: {product.ProductName} - Category: {product.Category} - Price: {product.Price}");
      }

      promptBuilder.AppendLine();
      promptBuilder.AppendLine("Here is the list of orders:");

      // list orders
      foreach (var order in orders)
      {
        promptBuilder.AppendLine($"Order ID: {order.OrderId} - Customer ID: {order.CustomerId} - Product ID: {order.ProductId} - Quantity: {order.Quantity}");
      }

      promptBuilder.AppendLine();

      return promptBuilder.ToString();
    }
  }
}