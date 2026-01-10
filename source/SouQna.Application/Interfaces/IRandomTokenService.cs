namespace SouQna.Application.Interfaces
{
    public interface IRandomTokenService
    {
        string Generate(int length);
    }
}