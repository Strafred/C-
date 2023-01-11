using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Labs;

public static class Program
{
    private static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureHostConfiguration(configHost =>
            {
                configHost.SetBasePath(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName);
            })
            .ConfigureServices((_, services) =>
            {
                services.AddHostedService<Princess>();
                services.AddSingleton<IContenderGenerator, DefaultContenderGenerator>();
                services.AddTransient<IHall, DefaultHall>();
                services.AddTransient<IFriend, DefaultFriend>();
            });
    }
}