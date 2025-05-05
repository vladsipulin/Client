namespace Client.Models
{
    public class UserRecover
    {
        public int Id { get; set; } // НКл или НС
        public string ЭлПочта { get; set; }
        public string ФИО { get; set; }
        public string Логин { get; set; }
        public string Пароль { get; set; }
        public string UserType { get; set; }

        public UserRecover(string ЭлПочта = "<ЭлПочта>", string ФИО = "<ФИО>", int Id = 0, string Логин = null, string Пароль = null, string UserType = null)
        {
            this.Id = Id;
            this.ЭлПочта = ЭлПочта;
            this.ФИО = ФИО;
            this.Логин = Логин;
            this.Пароль = Пароль;
            this.UserType = UserType;
        }
    }
}