using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace RecruitmentTest
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = BuildWebHost(args);
            await host.PrepareDatabase();
            await host.RunAsync();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
