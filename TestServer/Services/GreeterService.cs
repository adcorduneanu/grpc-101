namespace TestServer.Services
{
	using Domain;
	using TestServer.Domain.Services;

	public class GreeterService : IGreeterService
	{
		public string Ping()
		{
			return "Pong";
		}
	}
}
