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

        public static void AgainstNegativeOrZero<T>(T value, string parameterName)
            where T : struct, IComparable<T>
        {
            if(value.CompareTo(default) <= 0)
                throw new ArgumentException($"{parameterName} must be greater than zero", parameterName);
        }
    }
}