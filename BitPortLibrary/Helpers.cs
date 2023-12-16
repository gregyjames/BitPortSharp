using System.Text;

namespace BitPortLibrary;

internal static class Helpers
{
    public static string EncodeToBase64(string input)
    {
        byte[] bytesToEncode = Encoding.UTF8.GetBytes(input);
        return Convert.ToBase64String(bytesToEncode);
    }
}