using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace DuongTruong.IdentityServer.UI.Configurations;

public static partial class IdentityServerConfigurations
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>()
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>()
        {
            new ApiScope(name: "api1", displayName: "MyAPI"),
            new ApiScope(name: "demoapi", displayName: "Demo API"),
        };

    public static IEnumerable<Client> Clients =>
        new List<Client>()
        {
            new Client
            {
                ClientId = "demoweb",
                ClientSecrets = { new Secret("secret".Sha256()) },

                AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
            
                // where to redirect to after login
                RedirectUris =
                {                     
                    "https://localhost:31403/signin-oidc",
                    // add uri for testing.
                    "https://oauth.pstmn.io/v1/browser-callback",
                    "https://localhost:7292/swagger/oauth2-redirect.html",
                },

                AllowedCorsOrigins =
                {
                    // Allow origins for testing.
                    "https://jwt.io",
                    "https://localhost:7292",
                },
                
                // where to redirect to after logout
                PostLogoutRedirectUris =
                {
                    "https://localhost:31403/signout-callback-oidc"
                },

                AllowOfflineAccess = true,

                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "demoapi",
                },
            },
            // interactive ASP.NET Core Web App
            new Client
            {
                ClientId = "web",
                ClientSecrets = { new Secret("secret".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,
            
                // where to redirect to after login
                RedirectUris = { "https://localhost:5002/signin-oidc" },

                // where to redirect to after logout
                PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },

                AllowOfflineAccess = true,

                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "api1",
                }
            },
        };
}