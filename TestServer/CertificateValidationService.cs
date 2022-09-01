using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace TestServer
{
    internal class CertificateValidationService
    {
        internal bool ValidateCertificate(X509Certificate2 clientCertificate)
        {
            Trace.WriteLine(clientCertificate.FriendlyName);
            return true;
        }
    }
}