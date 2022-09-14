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

    public static IEnumerable<ApiResource> ApiResources => new List<ApiResource>()
    {
        new ApiResource("urn:demoapi", "DemoApp Api")
        {
            Scopes =
            {
                "urn:demoapi.write",
                "urn:demoapi.read",
            },
            // Expected to enable Resource Isolation by set RequireResourceIndicator = true
            ShowInDiscoveryDocument = true,
        }
    };

    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>()
        {
            new ApiScope(name: "api1", displayName: "MyAPI"),
            new ApiScope(name: "urn:demoapi.read", displayName: "Demo API Read"),
            new ApiScope(name: "urn:demoapi.write", displayName: "Demo API Write"),
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
                    "urn:demoapi",
                    "urn:demoapi.read",
                    "urn:demoapi.write",
                    "api1"
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