namespace TestServer.Core.App
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Security.Cryptography.X509Certificates;
	using Certificates;

	public class SampleCertificateThumbprintsValidationService : ICertificateValidationService
	{
		private readonly HashSet<string> validThumbprints = new[]
		{
			"1A04D12F431A6124BFB6F5B56489C67E922222EA",
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
