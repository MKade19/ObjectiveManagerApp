namespace ObjectiveManagerApp.Common.Models
{
    public class AuthData
    {
        public string Token { get; set; } = string.Empty;

        public string? Role { get; set; } = string.Empty;

        public AuthData(string token, string? role)
        {
            Token = token;
            Role = role;
        }

        public AuthData() { }
    }
}
