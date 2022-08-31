using GRPCContracts;

namespace GRPCServer.Services
{
    public class GreeterService : IGreeterService
    {
        public string Ping()
        {
            return "Pong";
        }
    }
}
