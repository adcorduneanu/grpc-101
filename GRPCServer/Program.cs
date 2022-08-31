using System;
using System.Linq;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using GRPCCore.Extensions;

namespace GRPCServer
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Run();
        }

        public static IWebHost CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseKestrel((builderContext, options) =>
                {
                   options.ConfigureHttpsDefaults(options => options.ClientCertificateMode = ClientCertificateMode.AllowCertificate);
                })
                .UseStartup<Startup>()
                .Build();
        }
    }
}
