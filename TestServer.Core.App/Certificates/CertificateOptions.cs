namespace TestServer.Core.App.Certificates
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Extensions.Configuration;
    using TestServer.Core.Certificates;

    public class CertificateOptions : ICertificateOptions
    {
        private readonly IConfiguration configuration;

        public CertificateOptions(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public HashSet<string> AllowedClients
        {
            get
            {
                return this.configuration
                    .GetSection("Clients:TrustedThumbprints")
                    .GetChildren()
                    .Select(x => x.Value)
                    .ToHashSet();
            }
        }
    }
}
