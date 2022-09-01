﻿namespace TestServer.Core.App
{
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.Server.Kestrel.Https;
	using Microsoft.Extensions.Hosting;

	public static class WebHostBuilderRunner
	{
		public static IHost CreateMutualTlsWebHostBuilder<TStartup>(string[] args) where TStartup : class
		{
			return Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<TStartup>();
					webBuilder.ConfigureKestrel(options =>
					{
						options.ConfigureHttpsDefaults(o =>
						{
							o.ClientCertificateMode = ClientCertificateMode.RequireCertificate;
						});
					});
				})
				.Build();
		}
	}
}