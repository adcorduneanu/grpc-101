namespace GRPCContracts
{
    using System.ServiceModel;

    [ServiceContract]
    public interface IGreeterService
    {
        [OperationContract]
        string Ping();
    }
}
