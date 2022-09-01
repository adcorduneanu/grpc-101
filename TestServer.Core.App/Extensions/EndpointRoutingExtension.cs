namespace TestServer.Core.App.Extensions
{
    using System.Linq;
    using System.ServiceModel;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Routing;
    using TestServer.Core.Extensions;

    public static class EndpointRoutingExtension
    {
        public static void MapAuthorizedGrpcService(this IEndpointRouteBuilder builder)
        {
            TypesExtensions.GetTypesWithAttribute<ServiceContractAttribute>()
                .SelectMany(x => x.GetImplementingTypes())
                .Select(type =>
                {
                    var method = typeof(GrpcEndpointRouteBuilderExtensions).GetMethod("MapGrpcService").MakeGenericMethod(type);
                    return (GrpcServiceEndpointConventionBuilder)method.Invoke(null, new[] { builder });
                }).ForEach(builder => builder.RequireAuthorization());
        }

        public static void MapAuthorizedControllers(this IEndpointRouteBuilder builder)
        {
            builder.MapControllers().RequireAuthorization();
        }
    }
}
