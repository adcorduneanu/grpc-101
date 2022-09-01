namespace TestServer.Domain.Services
{
	using System.ServiceModel;

	[ServiceContract]
	public interface IGreeterService
	{
		[OperationContract]
		string Ping();
	}
}
