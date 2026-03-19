namespace SouQna.Application.Interfaces
{
    public interface IWebhookParser
    {
        (bool Success, Guid OrderId, long IntentionOrderId) Parse(string json);
    }
}