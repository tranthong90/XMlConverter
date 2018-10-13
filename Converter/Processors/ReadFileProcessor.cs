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
using System.Threading.Tasks;

namespace Converter.Processors
{
    public class ReadFileProcessor : IReadFileProcessor
    {
        private readonly ConverterConfig _config;
        private readonly IXMLService _service;
        public ReadFileProcessor(IOptions<ConverterConfig> options, IXMLService service)
        {
            _config = options.Value;
            _service = service;
        }

        public async Task ReadFileAndPutToQueueAsync(ConcurrentQueue<XMLFile> queue)
        {
            var fileInfos = _service.ReadXMLFiles(_config.InputFolder);
            if (fileInfos == null)
                return;

            foreach (var file in fileInfos)
            {
                //load the file content
                await file.LoadDocumentAsync().ContinueWith(completed =>
                 {
                     queue.Enqueue(file);
                     Console.WriteLine($"Thread 1. Processed and added this file: {file.Name} to queue");

                 });
                await Task.Delay(100);
            }

        }

    }
}
