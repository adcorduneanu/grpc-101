namespace TestClient.Grpc
{
	using System;
	using System.Threading.Tasks;
	using global::Grpc.Net.Client;
	using ProtoBuf.Grpc.Client;
	using TestServer.Domain.Services;

	internal class Program
	{
		static async Task Main(string[] args)
		{
			var channel = GrpcChannel.ForAddress("https://localhost:5001");
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
	}
}
