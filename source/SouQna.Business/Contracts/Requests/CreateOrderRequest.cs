namespace SouQna.Business.Contracts.Requests
{
    public record CreateOrderRequest(
        string FullName,
        string AddressLine,
        string City,
        string PhoneNumber
    );
}