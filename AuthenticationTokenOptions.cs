using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HackTonTemplate
{
    public class AuthenticationTokenOptions
    {
        public const string ISSUER = "HackTonTemplate";
        public const string AUDIENCE = "HackTonTemplate";
        const string KEY = "secret_net_voxs_efw122efbgbfv32";
        public const int ACCESSLIFETIME = 1000;
        public const int REFRESHLIFETIME = 10000;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
