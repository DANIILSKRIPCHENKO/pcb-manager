using Microsoft.AspNetCore.Authentication;

namespace PcbManager.Main.WebHost.Security
{
    public class ApiKeyAuthenticationSchemeOptions : AuthenticationSchemeOptions
    {
        public string ApiKey { get; set; }
    }
}
