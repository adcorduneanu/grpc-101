using GRPCCore.App.Middlewares;
using GRPCServer.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf.Grpc.Server;

namespace GRPCServer
{
    internal class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCertificateAuthentication();
            services.AddAuthorization();
            services.AddGrpc();
            services.AddControllers();
            services.AddCodeFirstGrpc(config =>
            {
                config.ResponseCompressionLevel = System.IO.Compression.CompressionLevel.Optimal;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {           
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<GreeterService>();
                endpoints.MapControllers();
            });
        }
    }
}