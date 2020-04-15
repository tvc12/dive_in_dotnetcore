namespace AuthService.Domain
{
    public class LoginedUser
    {
        private string username;
        private string token;

        public LoginedUser(string username, string token)
        {
            this.Username = username;
            this.Token = token;
        }

        public string Username { get => username; set => username = value; }
        public string Token { get => token; set => token = value; }
    }
}