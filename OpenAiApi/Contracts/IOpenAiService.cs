namespace OpenAiApi.Contracts
{
  public interface IOpenAiService
  {
    Task<string> AskOpenAI(string userPrompt, string systemPrompt);

    Task<string> ConvertDataToPrompt();
  }
}