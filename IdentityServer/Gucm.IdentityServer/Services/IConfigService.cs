using Gucm.IdentityServer.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gucm.IdentityServer.Services
{
    public interface IConfigService
    {
        List<ClientConfig> GetClients();
        List<ApiResourceConfig> GetApiResources();

        void AddClients(List<ClientConfig> clients);
        void AddApiResources(List<ApiResourceConfig> apiResources);

        // TODO: Add IdentityResources from app settings (remove IdentityServerConfiguration (?))
        void AddIdentityResources();

        // TODO: Updates
        //void UpdateClients(List<ClientConfig> clients);
        //void UpdateApiResources(List<ApiResourceConfig> apiResources);
        //void UpdateIdentityResources();
    }
}
