namespace AuthService.Domain
{
    public class User
    {
        private string username;
        private string password;
        private string role;
        private string name;

        public User(string username, string password, string role, string name)
        {
            this.username = username;
            this.password = password;
            this.role = role;
            this.name = name;
        }

        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string Role { get => role; set => role = value; }
        public string Name { get => name; set => name = value; }
    }
}