using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OpenAiApi.Contracts;
using System.Text;

namespace OpenAiApi.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ChatController : ControllerBase
  {
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;
    private readonly IDatabaseService _databaseService;
    private readonly string _apiKey;

    public ChatController(HttpClient httpClient, IDatabaseService databaseService, IConfiguration configuration)
    {
      _httpClient = httpClient;
      _databaseService = databaseService;
      _configuration = configuration;
      _apiKey = _configuration.GetSection("OpenAI:ApiKey").Value;
    }

    [HttpPost("process-data")]
    public async Task<IActionResult> ProcessData([FromBody] string userPrompt)
    {
      // Convert data to a format suitable for OpenAI API
      var systemPrompt = await ConvertDataToPrompt();

      // Send data to OpenAI API and get the response
      var openAIResponse = await AskOpenAI(userPrompt, systemPrompt);

      return Ok(openAIResponse);
    }

    private async Task<string> AskOpenAI(string userPrompt, string systemPrompt)
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

    private async Task<string> ConvertDataToPrompt()
    {
      StringBuilder promptBuilder = new StringBuilder();

      // Fetch data from the database
      var customers = await _databaseService.GetCustomers();
      var products = await _databaseService.GetProducts();
      var orders = await _databaseService.GetOrders();

      promptBuilder.Append("You answer questions asked by employees that need to have rapid answers to questions about the customers, the orders and the products.");
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