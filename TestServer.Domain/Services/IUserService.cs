namespace TestServer.Domain.Services
{
	using System.ServiceModel;
	using System.Threading.Tasks;
	using Models;

	[ServiceContract]
	public interface IUserService
	{
		[OperationContract]
		Task<User> GetUserAsync(string name);
	}
}
