using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.IdentityServer
{
    public static class IdentityServerConfig
    {
        /// <summary>
        /// 资源
        /// </summary>
        public static IEnumerable<ApiResource> ApiResource { get { return GetApiResource(); } }

        /// <summary>
        /// 范围
        /// </summary>
        public static IEnumerable<ApiScope> ApiScope { get { return GetApiScope(); } }

        /// <summary>
        /// 客户端
        /// </summary>
        public static IEnumerable<Client> Client { get { return GetClient(); } }


        private static IEnumerable<ApiResource> GetApiResource() {

            return new List<ApiResource>()
            {
               new ApiResource( "WebSite"){ Scopes=new List<string>(){ "WebSite" } }
            };
        }

        private static IEnumerable<ApiScope> GetApiScope()
        {

            return new List<ApiScope>()
            {
               new ApiScope( "WebSite")
            };
        }

        private static IEnumerable<Client> GetClient()
        {
            return new List<Client>
                    {
                        new Client
                        {
                            ClientId = "client",
                            AllowedGrantTypes = GrantTypes.ClientCredentials,
                            ClientSecrets =
                            {
                                new Secret("secret".Sha256())
                            },
                            AllowedScopes = { "WebSite" }
                        }
                    };
        }
    }
}
