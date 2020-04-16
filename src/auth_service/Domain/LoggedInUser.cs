namespace AuthService.Domain
{
    public class LoggedInUser
    {
        private string username;
        private string token;

        private string role = "";

        public LoggedInUser(string username, string token, string role)
        {
            this.username = username;
            this.token = token;
            this.role = role;
        }

        public LoggedInUser(string username, string token)
        {
            this.username = username;
            this.token = token;
        }

        public string Username { get => username; }
        public string Token { get => token; }
        public string Role { get => role;  }
    }
}