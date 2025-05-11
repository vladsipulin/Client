using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace Client.Services
{
    internal class PasswordHasher
    {
        // Читаем перец из App.config
        private static readonly string Pepper = ConfigurationManager.AppSettings["PasswordPepper"]
            ?? throw new ConfigurationErrorsException("PasswordPepper not found in App.config");

        // Метод для генерации детерминированной соли
        public static string GenerateSalt(string userIdentifier)
        {
            using (var sha256 = SHA256.Create())
            {
                var identifierBytes = Encoding.UTF8.GetBytes(userIdentifier);
                var saltBytes = sha256.ComputeHash(identifierBytes);
                return Convert.ToBase64String(saltBytes);
            }
        }

        // Метод для хеширования пароля
        public static string HashPassword(string password, int userIdentifier)
        {
            var salt = GenerateSalt(userIdentifier.ToString());

            using (var sha256 = SHA256.Create())
            {
                // Конкатенируем пароль, соль и перец
                var saltedPepperedPassword = password + salt + Pepper;
                var saltedPepperedPasswordBytes = Encoding.UTF8.GetBytes(saltedPepperedPassword);

                // Вычисляем хеш
                var hashBytes = sha256.ComputeHash(saltedPepperedPasswordBytes);

                // Преобразуем хеш в строку Base64
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}	

