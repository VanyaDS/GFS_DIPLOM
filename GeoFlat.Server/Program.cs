using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace GeoFlat.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Photos"))
            {
                string folderName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Photos");
                Directory.CreateDirectory(folderName);
            }

            CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseKestrel(opts =>
                    {
                        opts.ListenAnyIP(5050);
                    });
                });
    }
}
