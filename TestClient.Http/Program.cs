namespace TestHttpClient
{
	using System;
	using System.Net.Http;
	using System.Security.Cryptography.X509Certificates;
	using System.Threading.Tasks;

	internal class Program
	{
		static async Task Main(string[] args)
		{
			var handler = new WinHttpHandler();
			handler.ClientCertificates.Add(LoadSslCertificate());
			handler.ServerCertificateValidationCallback = (requestMessage, certificate, x509Chain, sslPolicyErrors) => true;
			var httpClient = new HttpClient(handler);
			Console.WriteLine(await httpClient.GetStringAsync("https://localhost:5001/WeatherForecast/GetAuthWeather"));

			Console.ReadLine();
		}

		private static X509Certificate2 LoadSslCertificate()
		{
			using var certStore = new X509Store(StoreLocation.LocalMachine);
			certStore.Open(OpenFlags.ReadOnly);
			var cert = certStore.Certificates.Find(X509FindType.FindBySubjectName, "client-http", true)[0];
			certStore.Close();

			return cert;
		}
	}
}
