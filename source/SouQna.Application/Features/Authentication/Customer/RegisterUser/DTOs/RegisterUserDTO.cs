namespace SouQna.Application.Features.Authentication.Customer.RegisterUser.DTOs
{
    public record RegisterUserDTO(
        Guid Id,
        string FirstName,
        string LastName,
        string Email
    );
}