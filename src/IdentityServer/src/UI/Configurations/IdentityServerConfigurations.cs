using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using DuongTruong.IdentityServer.Core.Helpers;
using DuongTruong.IdentityServer.Core.Options;

namespace DuongTruong.IdentityServer.UI.Configurations;

public static class IdentityServerConfigurations
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>()
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource()
            {
                Name = RbacDefaults.Scope,
                DisplayName = "Your user roles",
                Required = true,
                UserClaims =
                {
                    RbacDefaults.RoleClaim,
                },
            }
        };

    public static IEnumerable<ApiResource> ApiResources => new List<ApiResource>()
    {
        new ApiResourceBuilder("urn:demoapi", "Demo Api Resource")
            .EnableRbac()
            .AllowScopes(
                "urn:demoapi:write",
                "urn:demoapi:read")
            .Build(),
    };

    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>()
        {
            new ApiScope(name: "api1", displayName: "My API"),
            new ApiScope(name: "urn:demoapi:read", displayName: "Demo API Read Permission"),
            new ApiScope(name: "urn:demoapi:write", displayName: "Demo API Write Permission"),
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

                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    RbacDefaults.Scope,
                    "urn:demoapi:read",
                    "urn:demoapi:write",
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

                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "api1",
                },
            },
        };
}
