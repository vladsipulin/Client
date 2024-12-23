using System;
using System.Security.Cryptography;
using System.Text;

namespace Client.Services
{
    internal class PasswordHasher
    {
        // Метод для генерации случайной соли
        public static string GenerateSalt(int size = 16)
        {
            var rng = new RNGCryptoServiceProvider();
            var saltBytes = new byte[size];
            rng.GetBytes(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }

        // Метод для хеширования пароля с использованием соли
        public static string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                // Конкатенируем пароль с солью
                var saltedPassword = password + salt;
                var saltedPasswordBytes = Encoding.UTF8.GetBytes(saltedPassword);

                // Вычисляем хеш
                var hashBytes = sha256.ComputeHash(saltedPasswordBytes);

                // Преобразуем хеш в строку Base64
                return Convert.ToBase64String(hashBytes);
            }
        }

        // Метод для проверки пароля
        public static bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            var hashOfEnteredPassword = HashPassword(enteredPassword, storedSalt);
            return hashOfEnteredPassword == storedHash;
        }
    }
}
