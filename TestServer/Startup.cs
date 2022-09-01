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

			app.UseEndpoints(
				endpoints =>
                {
					//	endpoints.MapControllers().RequireAuthorization(); 
					//	endpoints.MapGrpcService<GreeterService>().RequireAuthorization(); 
					//	endpoints.MapGrpcService<UserService>().RequireAuthorization();
					endpoints.MapAuthorizedGrpcService();
					endpoints.MapAuthorizedControllers();
                }
			);
		}
	}
}