using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using Environment = System.Environment;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Converter.IoC;
using System.IO;
using Converter.Settings;
using Converter.Interfaces;
using Converter.Processors;
using Converter.Services.Services;
using Converter.Services.Interfaces;

namespace Converter
{
    class Program
    {
        public static void Main(string[] args)
        {
            // create service collection
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            // create service provider
            var serviceProvider = serviceCollection.BuildServiceProvider();

            // run app
            serviceProvider.GetService<App>().Run().Wait();

            Console.ReadKey();
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddLogging();

            // build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            serviceCollection.AddOptions();
            serviceCollection.Configure<ConverterConfig>(configuration.GetSection("ConverterConfig"));

            // add services
            serviceCollection.AddSingleton<IReadFileProcessor, ReadFileProcessor>();
            serviceCollection.AddSingleton<IConvertFileProcessor, ConvertFileProcessor>();
            serviceCollection.AddSingleton<IXMLService, XMLService>();

            serviceCollection.AddTransient<App>();
        }

    }
}
