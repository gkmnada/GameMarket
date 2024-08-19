using Duende.IdentityServer.Models;

namespace IdentityService;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("FullPermission"),
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            // m2m client credentials flow client
            new Client
            {
                ClientId = "postman",
                ClientName = "postman",

                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets = new[] { new Secret("ResourceOwnerPassword".Sha256()) },

                RedirectUris = { "https://www.getpostman.com/oauth2/callback" },

                AllowedScopes = { "openid", "profile", "FullPermission" }
            },
        };
}
