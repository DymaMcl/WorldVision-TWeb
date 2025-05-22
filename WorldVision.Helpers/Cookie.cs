using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace WorldVision.Security
{
    public static class SecureTokenManager
    {
        private const string SaltSeed =
            "Nf8aQzLk9pXcRUG7sAbYZwVj" +
            "LmBHTx9cWeuV5pJqkFYDZa3M" +
            "oEdSLxCbmRtPv9gAFhJkNvTs" +
            "KhWBxRY2GTzDAcXwVLPkjbqZ";

        private static readonly byte[] Salt = Encoding.UTF8.GetBytes(SaltSeed);

        public static string GenerateToken(string input)
        {
            return Encrypt(input, GetSecretKey());
        }

        public static string ValidateToken(string encrypted)
        {
            return Decrypt(encrypted, GetSecretKey());
        }

        private static string GetSecretKey()
        {
            // Poate fi mutat în config
            return "xF7G9tKLqsY2WceN3pJfVKrbMZPqYe5QXG4E2Rt7T9sVcUK1LgHwJbnAvz53Upcy";
        }

        private static string Encrypt(string plainText, string password)
        {
            if (string.IsNullOrWhiteSpace(plainText)) throw new ArgumentNullException(nameof(plainText));
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException(nameof(password));

            using var aes = Aes.Create();
            var key = new Rfc2898DeriveBytes(password, Salt, 10000);
            aes.Key = key.GetBytes(aes.KeySize / 8);
            aes.GenerateIV();

            using var ms = new MemoryStream();
            ms.Write(BitConverter.GetBytes(aes.IV.Length), 0, sizeof(int));
            ms.Write(aes.IV, 0, aes.IV.Length);

            using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            using (var sw = new StreamWriter(cs))
            {
                sw.Write(plainText);
            }

            return Convert.ToBase64String(ms.ToArray());
        }

        private static string Decrypt(string cipherText, string password)
        {
            if (string.IsNullOrWhiteSpace(cipherText)) throw new ArgumentNullException(nameof(cipherText));
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException(nameof(password));

            var cipherBytes = Convert.FromBase64String(cipherText);
            using var ms = new MemoryStream(cipherBytes);

            var ivLengthBytes = new byte[sizeof(int)];
            if (ms.Read(ivLengthBytes, 0, ivLengthBytes.Length) != ivLengthBytes.Length)
                throw new FormatException("Invalid IV length in encrypted data.");

            var ivLength = BitConverter.ToInt32(ivLengthBytes, 0);
            var iv = new byte[ivLength];
            if (ms.Read(iv, 0, iv.Length) != iv.Length)
                throw new FormatException("IV data missing or corrupted.");

            using var aes = Aes.Create();
            var key = new Rfc2898DeriveBytes(password, Salt, 10000);
            aes.Key = key.GetBytes(aes.KeySize / 8);
            aes.IV = iv;

            using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }
    }
}
