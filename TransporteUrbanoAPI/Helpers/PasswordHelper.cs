using System.Security.Cryptography;
using System.Text;

namespace TransporteUrbanoAPI.Helpers;

public static class PasswordHelper
{
    // Igual que CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', ...), 2) de SQL Server
    public static string Hash(string password)
    {
        return Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(password)));
    }
}
