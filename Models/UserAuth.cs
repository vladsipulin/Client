using System;
using Client.Services;

namespace Client.Models
{
    public class UserAuth : DbClient
    {
        public string UserType { get; set; }

        // Конструктор для создания нового пользователя с сырым паролем
        public UserAuth(int НКл = 0, string Логин = "<Логин>", string Пароль = "<Пароль>", string Email = "<Email>", string UserType = "<UserType>")
        {
            this.НКл = НКл;
            this.Логин = Логин;
            this.Пароль = GetPasswordHash(Пароль);
            this.Email = Email;
            this.UserType = UserType;
        }

        // Конструктор для считывания данных из БД с уже хэшированным паролем
        public UserAuth(int НКл, string Логин, string hashedPassword, string Email, string UserType, bool isHashed)
        {
            this.НКл = НКл;
            this.Логин = Логин;
            this.Пароль = hashedPassword; // Пароль уже хэшированный
            this.Email = Email;
            this.UserType = UserType;
        }

        private string GetPasswordHash(string password)
        {
            string salt = "TfbcZEIwOHJokZyDIvOqjg==";
            return PasswordHasher.HashPassword(password, salt);
        }

        public static UserAuth GetClone(UserAuth userAuth)
        {
            if (userAuth is null)
                throw new ArgumentNullException(nameof(userAuth));

            return new UserAuth(userAuth.НКл, userAuth.Логин, userAuth.Пароль, userAuth.Email, userAuth.UserType, true)
            {
                ФИО = userAuth.ФИО,
                Пол = userAuth.Пол,
                ДатаРождения = userAuth.ДатаРождения
            };
        }

        public override string ToString()
        {
            return $"{НКл}: login: {Логин} password: {Пароль} email: {Email} userType: {UserType}";
        }
    }
}