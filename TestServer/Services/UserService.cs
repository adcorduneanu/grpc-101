namespace TestServer.Services
{
    using AutoFixture;
    using Domain.Models;
    using Domain.Services;
    using Microsoft.AspNetCore.Authorization;

    public class UserService : IUserService
    {
        [Authorize]
        public Task<User> GetUserAsync(
            string name
        )
        {
            var fixture = new Fixture();
            var address = fixture.Create<Address>();
            var user = new User(Random.Shared.Next(0, int.MaxValue), name, "last", address);
            return Task.FromResult(user);
        }
    }
}
