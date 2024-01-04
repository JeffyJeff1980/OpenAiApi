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
    private readonly IOpenAiService _openAiService;

    public ChatController(IOpenAiService openAiService)
    {
      _openAiService = openAiService;
    }

    [HttpPost("process-data")]
    public async Task<IActionResult> ProcessData([FromBody] string userPrompt)
    {
      // Convert data to a format suitable for OpenAI API
      var systemPrompt = await _openAiService.ConvertDataToPrompt();

      // Send data to OpenAI API and get the response
      var openAIResponse = await _openAiService.AskOpenAI(userPrompt, systemPrompt);

      return Ok(openAIResponse);
    }
  }
}