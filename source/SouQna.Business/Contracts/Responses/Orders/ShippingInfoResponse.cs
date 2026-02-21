namespace SouQna.Business.Contracts.Responses.Orders
{
    public record ShippingInfoResponse(
        string FullName,
        string AddressLine,
        string City,
        string PhoneNumber
    );
}