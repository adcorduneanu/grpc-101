namespace TestServer.Services
{
	using Domain;

	public class GreeterService : IGreeterService
	{
		public string Ping()
		{
			return "Pong";
		}
	}
}
