namespace TestServer.Core.App.Certificates
{
    using System.Security.Cryptography.X509Certificates;
    using TestServer.Core.Certificates;

    public class CertificateValidationService : ICertificateValidationService
    {
        private readonly ICertificateOptions certificateOptions;

        public CertificateValidationService(ICertificateOptions certificateOptions)
        {
            this.certificateOptions = certificateOptions;
        }

        public bool ValidateCertificate(X509Certificate2 clientCertificate)
        {
            return this.ValidateCertificate(clientCertificate.Thumbprint);
        }

        public bool ValidateCertificate(string thumbprint)
        {
            return this.certificateOptions.AllowedClients.Contains(thumbprint);
        }
    }
}
