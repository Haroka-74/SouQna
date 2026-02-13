namespace SouQna.Business.Interfaces
{
    public interface IValidationService
    {
        Task ValidateAsync<T>(T model);
    }
}