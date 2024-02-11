namespace ObjectiveManagerApp.Common.Models
{
    public class TokenPayload
    {
        public string Username { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;

        public TokenPayload(string username, string roleName)
        {
            Username = username;
            RoleName = roleName;
        }

        public TokenPayload() { }
    }
}
