using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Utils
{
    public static class PasswordValidation
    {
        public static string Password {get; set;}

        public static bool IsValidPassword(string password)
        {
            // Список предсказуемых паролей
            var predictablePasswords = new HashSet<string>
            {
                "123456", "123456789", "qwerty", "12345", "password",
                "12345678", "qwerty123", "1q2w3e", "111111", "1234567890"
            };

            // Проверка длины пароля
            if (password.Length < 16)
                return false;

            // Проверка на предсказуемые пароли
            if (predictablePasswords.Contains(password.ToLower()))
                return false;

            // Проверка на повторяющиеся или одинаковые символы
            if (HasRepeatingOrSequentialPatterns(password))
                return false;

            // Проверка наличия необходимых типов символов
            bool hasUpperCase = password.Any(char.IsUpper);
            bool hasLowerCase = password.Any(char.IsLower);
            bool hasDigit = password.Any(char.IsDigit);
            bool hasSpecialChar = password.Any(c => "!@#$%^&*()-_=+[]{}|;:,.<>?".Contains(c));

            return hasUpperCase && hasLowerCase && hasDigit && hasSpecialChar;
        }

        // Вспомогательный метод для проверки повторяющихся или одинаковых символов
        public static bool HasRepeatingOrSequentialPatterns(string password)
        {
            // Проверка на одинаковые символы (например, аааа или 1111)
            for (int i = 0; i < password.Length - 3; i++)
            {
                if (password[i] == password[i + 1] &&
                    password[i] == password[i + 2] &&
                    password[i] == password[i + 3])
                {
                    return true;
                }
            }

            // Проверка на повторяющиеся группы (например, 111222333)
            for (int groupSize = 2; groupSize <= password.Length / 2; groupSize++)
            {
                for (int i = 0; i <= password.Length - 2 * groupSize; i++)
                {
                    string group1 = password.Substring(i, groupSize);
                    string group2 = password.Substring(i + groupSize, groupSize);
                    if (group1 == group2)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
