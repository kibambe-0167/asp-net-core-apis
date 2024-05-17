namespace assessment.Models
{
    public class User
    {
        public int ID { get; set; }
        public string UserName {get;set;}
        public string Email { get; set;}
        public string Password { get; set;}

        public User()
        {
            ID = 0;
            UserName = "";
            Email = string.Empty;
            Password = string.Empty;
        }
    }
}
