namespace TestClient.Grpc
{
	using System;
	using System.Net.Http;
	using System.Security.Cryptography.X509Certificates;
	using System.Threading.Tasks;
	using global::Grpc.Net.Client;
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
					ClientCertificates = { LoadSslCertificate() }
				}
			});
			var greeterService = channel.CreateGrpcService<IGreeterService>();

			Console.WriteLine("ping...");
			Console.WriteLine(greeterService.Ping());
			Console.WriteLine();

			var userService = channel.CreateGrpcService<IUserService>();
			Console.WriteLine("Please type a name");
			var name = Console.ReadLine();

			Console.WriteLine(await userService.GetUserAsync(name));
			Console.ReadKey();
		}

		private static X509Certificate2 LoadSslCertificate()
		{
			using var certStore = new X509Store(StoreLocation.LocalMachine);
			certStore.Open(OpenFlags.ReadOnly);
			var cert = certStore.Certificates.Find(X509FindType.FindBySubjectName, "client", true)[0];
			certStore.Close();

			return cert;
		}
	}
}
