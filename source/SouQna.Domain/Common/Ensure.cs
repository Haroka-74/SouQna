namespace SouQna.Domain.Common
{
    public static class Ensure
    {
        public static void That(bool condition, string message)
        {
            if (!condition)
                throw new InvalidOperationException(message);
        }

        public static void Not(bool condition, string message)
        {
            if (condition)
                throw new InvalidOperationException(message);
        }

        public static void NotNullOrEmpty(string? value, string message)
        {
            if (string.IsNullOrEmpty(value))
                throw new InvalidOperationException(message);
        }
    }
}