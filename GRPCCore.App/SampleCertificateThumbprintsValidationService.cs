using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using GRPCCore.Certificates;
using GRPCCore.Extensions;

namespace GRPCCore.App
{
    public class SampleCertificateThumbprintsValidationService : ICertificateValidationService
    {
        private readonly HashSet<string> validThumbprints = new[]
        {
            "141594A0AE38CBBECED7AF680F7945CD51D8F28A",
            "0C89639E4E2998A93E423F919B36D4009A0F9991",
            "BA9BF91ED35538A01375EFC212A2F46104B33A44"
        }.ToHashSet<string>();

        public bool ValidateCertificate(X509Certificate2 clientCertificate)
        {
            return validThumbprints.Contains(clientCertificate.Thumbprint);
        }

        public bool ValidateCertificate(string thumbprint)
        {
            return validThumbprints.Contains(thumbprint);
        }
    }
}
