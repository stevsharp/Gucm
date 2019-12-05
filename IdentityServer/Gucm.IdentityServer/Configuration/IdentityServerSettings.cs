using System.Collections.Generic;

namespace Gucm.IdentityServer.Configuration
{
    public class IdentityServerSettings
    {
        /// <summary>
        /// OpenID Connect or OAuth2 clients
        /// </summary>
        public List<ClientConfig> Clients { get; set; }
        /// <summary>
        /// Web API resources
        /// </summary>
        public List<ApiResourceConfig> ApiResources { get; set; }

        public IdentityServerSettings()
        {
            Clients = new List<ClientConfig>();
            ApiResources = new List<ApiResourceConfig>();
        }
    }
}
}
