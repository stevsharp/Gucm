using System;
using AutoMapper;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using Gucm.IdentityServer.Configuration;
using System.Linq;

namespace Gucm.IdentityServer.Services
{
    public class ConfigService : IConfigService
    {
        private readonly IMapper mapper;
        private readonly ConfigurationDbContext context;
        private readonly IdentityServerSettings settings;

        public ConfigService(IMapper mapper, ConfigurationDbContext context, IOptions<IdentityServerSettings> settings)
        {
            this.mapper = mapper;
            this.context = context;
            this.settings = settings.Value;
        }

        public List<ClientConfig> GetClients() => settings.Clients;

        public List<ApiResourceConfig> GetApiResources() => settings.ApiResources;

        public void AddClients(List<ClientConfig> clients)
        {
            if (!context.Clients.Any() && clients.Count > 0)
            {
                foreach (var clientConfig in clients)
                {
                    var client = mapper.Map<ClientConfig, Client>(clientConfig);

                    if (client.ClientSecrets != null && client.ClientSecrets.Count > 0)
                        client.ClientSecrets.ToList().ForEach(x => { x.Value = x.Value.ToSha256(); });

                    context.Clients.Add(client.ToEntity());
                }
                context.SaveChanges();
            }
        }

        public void AddApiResources(List<ApiResourceConfig> apiResources)
        {
            if (!context.ApiResources.Any() && apiResources.Count > 0)
            {
                foreach (var apiResourceConfig in apiResources)
                {
                    var apiResource = mapper.Map<ApiResourceConfig, ApiResource>(apiResourceConfig);

                    if (apiResource.ApiSecrets != null && apiResource.ApiSecrets.Count > 0)
                        apiResource.ApiSecrets.ToList().ForEach(x => { x.Value = x.Value.ToSha256(); });

                    context.ApiResources.Add(apiResource.ToEntity());
                }
                context.SaveChanges();
            }
        }

        // TODO: Add IdentityResources from app settings (remove IdentityServerConfiguration (?))
        public void AddIdentityResources()
        {
            if (!context.IdentityResources.Any())
            {
                foreach (var resource in GetIdentityResources())
                    context.IdentityResources.Add(resource.ToEntity());
                context.SaveChanges();
            }
        }

        private IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource("roles", "Roles", new[] {"role"})
            };
        }
    }
}
