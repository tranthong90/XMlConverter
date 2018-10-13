using Converter.Processors;
using Converter.Services.Interfaces;
using Converter.Services.Services;
using Converter.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Converter.IoC
{
    public static class BindingModule
    {
        public static IServiceCollection Register(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);
            //TODO: add Microsoft.Extensions.Options
            services.Configure<ConverterConfig>(configuration.GetSection("NotificationEngine"));
            services.AddSingleton<IXMLService, XMLService>();
            services.AddSingleton<ConvertFileProcessor>();
            services.AddSingleton<ReadFileProcessor>();
            return services;
        }
    }
}
