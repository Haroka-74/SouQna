namespace SouQna.Application.Features.Reviews.Customer.GetReviews.DTOs
{
    public record ReviewDTO(
        string ReviewerName,
        int Rating,
        string Body,
        DateTime CreatedAt
    );
}