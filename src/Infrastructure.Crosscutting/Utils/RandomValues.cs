namespace Infrastructure.Crosscutting.Utils;
public static class RandomValues
{
    private static Random random = new Random();

    public static string GetRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public static int GetRandomNumber(int length)
    {
        Random random = new Random();
        int randomNumber = random.Next(1, length);
        return randomNumber;
    }
}