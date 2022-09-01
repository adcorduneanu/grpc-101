namespace TestServer.Core.Certificates
{
    using System.Collections.Generic;

    public interface ICertificateOptions
    {
        HashSet<string> AllowedClients { get; }
    }
}
