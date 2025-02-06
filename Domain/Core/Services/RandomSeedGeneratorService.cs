using System.Security.Cryptography;
using System.Text;

namespace Domain.Core.Services
{
    public static class RandomSeedGeneratorService
    {
        static public int GenerarSeed(string input)
        {
            byte[] hashBytes;

            using (SHA256 sha256 = SHA256.Create())
            {
                hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            }

            // Convertir el hash a un entero (por ejemplo, tomando los primeros 4 bytes)
            return  BitConverter.ToInt32(hashBytes, 0);
        }
    }
}