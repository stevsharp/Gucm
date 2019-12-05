using IdentityServer4.Models;
using System.Collections.Generic;

namespace Gucm.IdentityServer.Configuration
{
    public class ClientConfig
    {
        /// <summary>
        /// Unique ID of the client
        /// </summary>
        public string ClientId { get; set; }
        /// <summary>
        /// Client display name (used for logging and consent screen)
        /// </summary>
        public string ClientName { get; set; }
        /// <summary>
        /// Description of the client
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Specifies whether a consent screen is required (defaults to true)
        /// </summary>
        public bool RequireConsent { get; set; }
        /// <summary>
        /// URI to further information about client (used on consent screen)
        /// </summary>
        public string ClientUri { get; set; }
        /// <summary>
        /// URI to client logo (used on consent screen)
        /// </summary>
        public string LogoUri { get; set; }
        /// <summary>
        ///  Lifetime of identity token in seconds (defaults to 300 seconds / 5 minutes)
        /// </summary>
        public int IdentityTokenLifetime { get; set; }
        /// <summary>
        /// Lifetime of access token in seconds (defaults to 3600 seconds / 1 hour)
        /// </summary>
        public int AccessTokenLifetime { get; set; }
        /// <summary>
        ///  Lifetime of authorization code in seconds (defaults to 300 seconds / 5 minutes)
        /// </summary>
        public int AuthorizationCodeLifetime { get; set; }
        /// <summary>
        /// Controls whether access tokens are transmitted via the browser for this client
        /// (defaults to false). This can prevent accidental leakage of access tokens when
        /// multiple response types are allowed.
        /// </summary>
        public bool AllowAccessTokensViaBrowser { get; set; }
        /// <summary>
        /// Specifies the allowed grant types (legal combinations of AuthorizationCode, Implicit,
        /// Hybrid, ResourceOwner, ClientCredentials).
        /// </summary>
        public List<string> AllowedGrantTypes { get; set; }
        /// <summary>
        /// Specifies the api scopes that the client is allowed to request. If empty, the
        /// client can't access any scope
        /// </summary>
        public List<string> AllowedScopes { get; set; }
        /// <summary>
        /// Gets or sets the allowed CORS origins for JavaScript clients.
        /// </summary>
        public List<string> AllowedCorsOrigins { get; set; }
        /// <summary>
        /// Specifies allowed URIs to return tokens or authorization codes to
        /// </summary>
        public List<string> RedirectUris { get; set; }
        /// <summary>
        /// Specifies allowed URIs to redirect to after logout
        /// </summary>
        public List<string> PostLogoutRedirectUris { get; set; }
        /// <summary>
        /// Specifies logout URI at client for HTTP front-channel based logout.
        /// </summary>
        public string FrontChannelLogoutUri { get; set; }
        /// <summary>
        /// Client secrets - only relevant for flows that require a secret
        /// </summary>
        public List<Secret> ClientSecrets { get; set; }
    }
}
