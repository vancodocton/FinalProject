using Duende.IdentityServer.Models;
using DuongTruong.IdentityServer.Core.Helpers;

namespace DuongTruong.IdentityServer.Core.Options;

public class ApiResourceBuilder
{
    private bool _isBuilt = false;
    private readonly ApiResource _apiResource;

    public ApiResourceBuilder(ApiResource apiResource)
    {
        _apiResource = apiResource;
    }

    public ApiResourceBuilder(string name, string displayName)
    {
        _apiResource = new(name, displayName);
    }

    public ApiResourceBuilder EnableRbac()
    {
        _apiResource.UserClaims.Add(RbacDefaults.RoleClaim);
        return this;
    }

    public ApiResourceBuilder IncludeClaims(params string[] claims)
    {
        foreach (var claim in claims)
            _apiResource.UserClaims.Add(claim);
        return this;
    }

    public ApiResourceBuilder AllowScopes(params string[] scopes)
    {
        foreach (var scope in scopes)
            _apiResource.Scopes.Add(scope);
        return this;
    }

    public ApiResourceBuilder EnableResourceIsolation(bool isTrue = true)
    {
        _apiResource.RequireResourceIndicator = isTrue;
        return this;
    }

    public ApiResource Build()
    {
        if (_isBuilt)
            throw new InvalidOperationException("Api Resource had already build");

        _isBuilt = true;
        return _apiResource;
    }
}
