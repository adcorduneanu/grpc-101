namespace TestServer
{
	using Core.App;

	public static class Program
	{
		private static void Main(string[] args) => WebHostBuilderRunner.CreateMutualTlsWebHostBuilder<Startup>(args).Run();
	}
}