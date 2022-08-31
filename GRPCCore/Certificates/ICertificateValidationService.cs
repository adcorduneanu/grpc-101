using System.Security.Cryptography.X509Certificates;

namespace GRPCCore.Certificates
{
    public interface ICertificateValidationService
    {
        bool ValidateCertificate(X509Certificate2 clientCertificate);
        bool ValidateCertificate(string thumbprint);
    }
}
