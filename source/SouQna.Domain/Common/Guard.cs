namespace SouQna.Domain.Common
{
    public static class Guard
    {
        public static void AgainstNullOrEmpty(string value, string parameterName)
        {
            if(string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"{parameterName} cannot be null or empty", parameterName);
        }

        public static void AgainstNull<T>(T value, string parameterName)
            where T : class
        {
            if(value is null)
                throw new ArgumentNullException(parameterName, $"{parameterName} cannot be null");
        }

        public static void AgainstOutOfRange<T>(T value, T min, T max, string parameterName)
            where T : IComparable<T>
        {
            if (value.CompareTo(min) < 0 || value.CompareTo(max) > 0)
                throw new ArgumentOutOfRangeException(
                    parameterName,
                    $"{parameterName} must be between {min} and {max}"
                );
        }
    }
}