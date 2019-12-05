using IdentityServer4.Models;
using System.Collections.Generic;

namespace Gucm.IdentityServer.Configuration
{
    public class ApiResourceConfig
    {
        /// <summary>
        /// The unique name of the resource
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Display name of the resource.
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        ///  Description of the resource.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The API secret is used for the introspection endpoint. The API can authenticate
        /// with introspection using the API name and secret.
        /// </summary>
        public List<Secret> ApiSecrets { get; set; }
        /// <summary>
        ///  List of accociated user claims that should be included when this resource is requested
        /// </summary>
        public List<string> UserClaims { get; set; }
        /// <summary>
        /// An API must have at least one scope. Each scope can have different settings
        /// </summary>
        public List<Scope> Scopes { get; set; }
    }
}
