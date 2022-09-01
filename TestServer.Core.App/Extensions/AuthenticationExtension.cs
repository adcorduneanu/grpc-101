namespace TestServer.Core.App.Extensions
{
    using System;
    using System.Security.Claims;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading.Tasks;
    using Certificates;
    using Microsoft.AspNetCore.Authentication.Certificate;
    using Microsoft.Extensions.DependencyInjection;
    using TestServer.Core.Certificates;

    public static class AuthenticationExtension
    {
        public static void AddCertificateAuthentication(this IServiceCollection services)
        {
            services.AddSingleton<ICertificateOptions, CertificateOptions>();
            services.AddSingleton<ICertificateValidationService, CertificateValidationService>();


            services.AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme)
               .AddCertificate(opt =>
               {
                   opt.AllowedCertificateTypes = CertificateTypes.All;
                   opt.RevocationMode = X509RevocationMode.NoCheck;
                   opt.Events = new CertificateAuthenticationEvents()
                   {
                       OnCertificateValidated = context =>
                       {
                           var validationService = context.HttpContext.RequestServices.GetService<ICertificateValidationService>();

                           if (validationService != null && validationService.ValidateCertificate(context.ClientCertificate))
                           {
                               var claims = new[] {
                                        new Claim(ClaimTypes.NameIdentifier, context.ClientCertificate.Subject, ClaimValueTypes.String, context.Options.ClaimsIssuer),
                                        new Claim(ClaimTypes.Name, context.ClientCertificate.Subject, ClaimValueTypes.String, context.Options.ClaimsIssuer)
                                   };
                               context.Principal = new ClaimsPrincipal(new ClaimsIdentity(claims, context.Scheme.Name));
                               context.Success();
                           }
                           else
                           {
                               Console.WriteLine("invalid cert");
                               context.Fail("invalid cert");
                           }


                           return Task.CompletedTask;
                       },
                       OnAuthenticationFailed = context =>
                       {
                           context.Fail("invalid cert");

                           return Task.CompletedTask;
                       }
                   };
               }
           ).AddCertificateCache(
                options =>
                {
                    options.CacheSize = 1024;
                    options.CacheEntryExpiration = TimeSpan.FromHours(1);
                }
           );
        }
    }
}
