using System;
using Client.Services;

namespace Client.Models
{
    public class UserAuth : Employer
    {
        public string UserType { get; set; }

        public UserAuth(string Логин, string Пароль, int ID = -1, string UserType = "none")
        {
            this.ID = ID;
            this.Логин = Логин;
            this.Пароль = PasswordHasher.HashPassword(Пароль, ID);
            this.UserType = UserType;
        }

        public UserAuth(string Email = "<Email>", string ФИО= "<ФИО>", int ID = -1)
        {
            this.Email = Email;
            this.ФИО = ФИО;
            this.ID = ID;
            UserType = String.Empty;
        }

        public override string ToString()
        {
            return $"{ID}: login: {Логин} password: {Пароль} ";
        }
    }
}