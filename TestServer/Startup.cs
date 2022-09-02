namespace TestServer
{
	using Core.App.Extensions;
	using ProtoBuf.Grpc.Server;
	using Services;

	internal class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddCertificateAuthentication();

			services.AddControllers();
			services.AddHealthChecks();
			services.AddGrpc();
			services.AddCodeFirstGrpc(config =>
			{
				config.ResponseCompressionLevel = System.IO.Compression.CompressionLevel.Optimal;
			});

			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen();
			//https://martinbjorkstrom.com/posts/2020-07-08-grpc-reflection-in-net
			services.AddCodeFirstGrpcReflection();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseSwagger();
			app.UseSwaggerUI();

			app.UseHsts();
			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseHealthChecks("/health");

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseGrpcWeb();
			app.UseEndpoints(
				endpoints =>
				{
					endpoints.MapGrpcService<GreeterService>().RequireAuthorization().EnableGrpcWeb();
					endpoints.MapGrpcService<UserService>().RequireAuthorization().EnableGrpcWeb();
					//	endpoints.MapAuthorizedGrpcService();
					endpoints.MapAuthorizedControllers();

					//https://martinbjorkstrom.com/posts/2020-07-08-grpc-reflection-in-net
					endpoints.MapCodeFirstGrpcReflectionService();
				}
			);
		}
	}
}