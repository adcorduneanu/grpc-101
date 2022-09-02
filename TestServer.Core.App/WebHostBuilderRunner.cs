namespace TestServer.Core.App;

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.Extensions.Hosting;

public static class WebHostBuilderRunner
{
	public static void LoadAllAssemblies()
	{
		Directory.GetFiles(AppContext.BaseDirectory, "*.dll", SearchOption.TopDirectoryOnly)
			.Select(AssemblyLoadContext.Default.LoadFromAssemblyPath);
	}

	public static IHost CreateMutualTlsWebHostBuilder<TStartup>(
		string[] args
	) where TStartup : class
	{
		LoadAllAssemblies();

		return Host.CreateDefaultBuilder(args)
			.UseWindowsService()
			.ConfigureWebHostDefaults(
				webBuilder =>
				{
					webBuilder.UseStartup<TStartup>();
					webBuilder.ConfigureKestrel(
						options =>
						{
							//options.ConfigureEndpointDefaults(
							//	configureOptions =>
							//	{
							//		configureOptions.Protocols = HttpProtocols.Http1AndHttp2AndHttp3;
							//	}
							//);

							options.ConfigureHttpsDefaults(
								configureOptions =>
								{
									configureOptions.ClientCertificateMode = ClientCertificateMode.AllowCertificate;
								}
							);
						}
					);
				}
			)
			.Build();
	}
}