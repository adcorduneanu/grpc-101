namespace TestClient.Grpc
{
	using System;
	using System.IO;
	using System.Net.Http;
	using System.Runtime.ConstrainedExecution;
	using System.Security.Authentication;
	using System.Security.Cryptography.X509Certificates;
	using System.ServiceModel.Channels;
	using System.Text;
	using System.Threading.Tasks;
	using global::Grpc.Core;
	using global::Grpc.Net.Client;
    using Newtonsoft.Json;
    using ProtoBuf.Grpc.Client;
    using TestServer.Domain.Services;

    internal class Program
	{
		static async Task Main(string[] args)
		{
            var channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions
            {
                HttpHandler = new WinHttpHandler
                {
                    ClientCertificates = { LoadSslCertificate("client-grpc") }
                }
            });

            var greeterService = channel.CreateGrpcService<IGreeterService>();

			Console.WriteLine("ping...");
			Console.WriteLine(greeterService.Ping());
			Console.WriteLine();

			var userService = channel.CreateGrpcService<IUserService>();
			Console.WriteLine("Please type a name");
			var name = Console.ReadLine();

			Console.WriteLine(JsonConvert.SerializeObject(await userService.GetUserAsync(name), Formatting.Indented));

			Console.WriteLine("Press any key to exit...");
			Console.ReadKey();
		}

		private static X509Certificate2 LoadSslCertificate(string name, StoreName storeName = StoreName.My)
		{
			using var certStore = new X509Store(storeName, StoreLocation.LocalMachine);
			certStore.Open(OpenFlags.ReadOnly);
			var cert = certStore.Certificates.Find(X509FindType.FindBySubjectName, name, true)[0];
			certStore.Close();

			return cert;
		}

		public static string GetRootCertificates(string name)
		{
			StringBuilder builder = new StringBuilder();
			var cert = LoadSslCertificate(name);
			var rootCa = LoadSslCertificate(cert.Issuer.Replace("CN=",string.Empty), StoreName.Root);

			builder.AppendLine(
					"# Issuer: " + rootCa.Issuer.ToString() + "\n" +
					"# Subject: " + rootCa.Subject.ToString() + "\n" +
					"# Label: " + rootCa.FriendlyName.ToString() + "\n" +
					"# Serial: " + rootCa.SerialNumber.ToString() + "\n" +
					"# SHA1 Fingerprint: " + rootCa.GetCertHashString().ToString() + "\n" +
					ExportToPEM(rootCa) + "\n");
			return builder.ToString();
		}

		public static string ExportToPEM(X509Certificate cert)
		{
			StringBuilder builder = new StringBuilder();

			builder.AppendLine("-----BEGIN CERTIFICATE-----");
			builder.AppendLine(Convert.ToBase64String(cert.Export(X509ContentType.Cert), Base64FormattingOptions.InsertLineBreaks));
			builder.AppendLine("-----END CERTIFICATE-----");

			return builder.ToString();
		}
	}
}
