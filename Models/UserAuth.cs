using System;
using Client.Services;

namespace Client.Models
{
    public class UserAuth : Employer
    {
        public string UserType { get; set; }

        public UserAuth(int ID = 0, string Логин = "<Логин>", string Пароль = "<Пароль>")
        {
            this.ID = ID;
            this.Логин = Логин;
            this.Пароль = GetPasswordHash(Пароль);
        }

        public UserAuth(string Email = "<Email>", string ФИО= "<ФИО>", int ID = 0)
        {
            this.Email = Email;
            this.ФИО = ФИО;
            this.ID = ID;
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

            return new UserAuth(userAuth.ID, userAuth.Логин, userAuth.Пароль)
            {
                ФИО = userAuth.ФИО,
                Пол = userAuth.Пол,
                ДатаРождения = userAuth.ДатаРождения
            };
        }

        public override string ToString()
        {
            return $"{ID}: login: {Логин} password: {Пароль} ";
        }
    }
}