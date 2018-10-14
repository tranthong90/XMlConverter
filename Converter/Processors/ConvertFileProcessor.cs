using Converter.Core.Extensions;
using Converter.Core.Models;
using Converter.Interfaces;
using Converter.Services.Interfaces;
using Converter.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Converter.Processors
{
    public class ConvertFileProcessor : IConvertFileProcessor
    {
        private readonly ConverterConfig _config;
        public ConvertFileProcessor(IOptions<ConverterConfig> options)
        {
            _config = options.Value;
        }

        public async Task GetDataAndConvert(ConcurrentQueue<XMLFile> queue, string xsltString)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(10); // auto stop if waiting for more than 10s
            int elapsed = 0;
            while (elapsed < timeSpan.TotalMilliseconds)
            {
                if (queue.TryDequeue(out XMLFile file))
                {
                    elapsed = 0;
                    string htmlData = file.ConvertToHTML(xsltString);

                    string destinationPath = Path.Combine(_config.OutputFolder, file.GetHTMLFileName());

                    using (var stream = File.CreateText(destinationPath))
                    {
                        await stream.WriteAsync(htmlData);
                        await stream.FlushAsync();
                    }
                    Console.WriteLine($"Thread 2. Converted and write this file: {file.Name} to output folder");
                }else
                {
                    elapsed += 1000;
                    await Task.Delay(100); // to show task run concurrently 
                }
            }

        }


    }
}
