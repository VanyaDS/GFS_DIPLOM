using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace GeoFlat.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    //.UseSetting("https_port", "5080");
                    //webBuilder.UseKestrel(opts =>
                    //{
                    //    opts.ListenAnyIP(5079);
                    //});
                });
    }
}
