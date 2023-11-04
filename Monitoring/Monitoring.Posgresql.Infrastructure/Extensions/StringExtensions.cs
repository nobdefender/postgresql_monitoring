namespace Monitoring.Posgresql.Infrastructure.Extensions;

public static class StringExtensions
{
    public static long CalculateHash(this string text)
    {
        ulong hashedValue = 3074457345618258791ul;
        for (int i = 0; i < text.Length; i++)
        {
            hashedValue += text[i];
            hashedValue *= 3074457345618258799ul;
        }
        return unchecked((long)hashedValue + long.MinValue);
    }
}