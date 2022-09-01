﻿using System;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace TestClient
{
    internal class Program
    {
        private static string _sslThumbprint = "175B6BFA66B72F0B5F0C40F129226329F9F00B47"; // Your own SSL certificate thumbprint 

        static async Task Main(string[] args)
        {
            var handler = new HttpClientHandler
            {
                //                ClientCertificateOptions = ClientCertificateOption.Manual,
                //SslProtocols = SslProtocols.Tls12,
                //                ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4)=>  true
            };
            handler.ClientCertificates.Add(LoadSSLCertificate());
            var httpClient = new HttpClient(handler);
            Console.WriteLine(await httpClient.GetStringAsync("https://localhost:5001/WeatherForecast"));

            Console.ReadLine();
        }

        private static X509Certificate2 LoadSSLCertificate()
        {
            using var certStore = new X509Store(StoreLocation.LocalMachine);
            certStore.Open(OpenFlags.ReadOnly);
            var cert = certStore.Certificates.Find(X509FindType.FindBySubjectName, "client", true)[0];
            certStore.Close();

            return cert;
        }
    }
}