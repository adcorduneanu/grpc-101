namespace TestServer.Services
{
	using AutoFixture;
	using Domain.Models;
	using Domain.Services;

	public class UserService : IUserService
	{
		public Task<User> GetUserAsync(
			string name
		)
		{
			return Task.FromResult(new Fixture().Create<User>());
		}
	}
}
