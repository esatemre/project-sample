using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;

namespace TheProject.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel(opt =>
                {
                    opt.AddServerHeader = false; //remove server header to decrease response size
                    opt.Limits.MaxRequestBodySize = 1073741824; //MAX 1 GB for file upload
                    opt.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(30); //MAX 30 minutes without timeout for file upload
                })
                .UseWebRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>();
    }
}
