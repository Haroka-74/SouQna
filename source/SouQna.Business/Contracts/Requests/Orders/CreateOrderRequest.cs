namespace SouQna.Business.Contracts.Requests.Orders
{
    public record CreateOrderRequest(
        string FullName,
        string AddressLine,
        string City,
        string PhoneNumber
    );
}