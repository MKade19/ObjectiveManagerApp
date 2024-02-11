using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ObjectiveManagerApp.UI.Util
{
    public class AuthOptions
    {
        public const string Issuer = "MyIssuer";
        public const string Audience = "MyAudience";
        const string Key = "otE%Rbheug.trhter5ueubhe5rgher5hgoeurghugfrhersugbwre4uofther4uofgber4ufheriujfrufgerBVGitfwyugsli";
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
    }
}
