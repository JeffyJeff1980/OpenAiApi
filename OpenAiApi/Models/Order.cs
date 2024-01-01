namespace OpenAiApi.Models
{
  public class Order
  {
    public decimal OrderId { get; set; }
    public decimal CustomerId { get; set; }
    public decimal ProductId { get; set; }
    public decimal Quantity { get; set; }
  }
}