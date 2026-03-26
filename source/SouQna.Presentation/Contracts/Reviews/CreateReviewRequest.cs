namespace SouQna.Presentation.Contracts.Reviews
{
    public record CreateReviewRequest(
        int Rating,
        string Body
    );
}