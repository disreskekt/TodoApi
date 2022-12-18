using System.Security.Cryptography;
using System.Text;

namespace Api.Helpers;

public static class HashHelper
{
    public static string ComputeMd5Hash(string input)
    {
        using (MD5 md5 = MD5.Create())
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            
            return Convert.ToHexString(hashBytes);
        }
    }
}