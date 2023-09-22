using Duende.IdentityServer.Models;

namespace PcbManager.Security.WebHost
{
    public class Config
    {
        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "swaggerClient",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes =
                    {
                        Scopes.ImageRead,
                        Scopes.ImageWrite,
                        Scopes.ReportRead,
                        Scopes.ReportWrite,
                        Scopes.UserRead,
                        Scopes.UserWrite,
                        Scopes.AiWrite
                    }
                },
                new Client
                {
                    ClientId = "spaClient",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes =
                    {
                        Scopes.ImageRead,
                        Scopes.ImageWrite,
                        Scopes.ReportRead,
                        Scopes.ReportWrite,
                        Scopes.UserRead,
                        Scopes.UserWrite,
                        Scopes.AiWrite
                    }
                },
                new Client
                {
                    ClientId = "tgClient",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes =
                    {
                        Scopes.ImageRead,
                        Scopes.ImageWrite,
                        Scopes.ReportRead,
                        Scopes.ReportWrite,
                        Scopes.UserRead,
                        Scopes.UserWrite,
                        Scopes.AiWrite
                    }
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope(name: Scopes.ImageRead),
                new ApiScope(name: Scopes.ImageWrite),
                new ApiScope(name: Scopes.ReportRead),
                new ApiScope(name: Scopes.ReportWrite),
                new ApiScope(name: Scopes.UserRead),
                new ApiScope(name: Scopes.UserWrite),
                new ApiScope(name: Scopes.AiWrite)
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource("image", "Image API")
                {
                    Scopes = { Scopes.ImageRead, Scopes.ImageRead }
                },
                new ApiResource("report", "Report API")
                {
                    Scopes = { Scopes.ReportRead, Scopes.ReportWrite }
                },
                new ApiResource("user", "User API")
                {
                    Scopes = { Scopes.UserRead, Scopes.UserWrite }
                },
                new ApiResource("ai", "AI resource") { Scopes = { Scopes.AiWrite } }
            };
    }
}
