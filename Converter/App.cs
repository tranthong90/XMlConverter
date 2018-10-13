using Converter.Core.Models;
using Converter.Interfaces;
using Converter.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Converter
{
    public class App
    {
        private readonly ILogger<App> _logger;
        private readonly ConverterConfig _config;
        private readonly IReadFileProcessor _readFile;
        private readonly IConvertFileProcessor _convertFile;
        public App(ILogger<App> logger, IOptions<ConverterConfig> options, IReadFileProcessor readFile, IConvertFileProcessor convertFile)
        {
            _logger = logger;
            _config = options.Value;
            _readFile = readFile;
            _convertFile = convertFile;
        }
        public async Task Run()
        {
            ConcurrentQueue<XMLFile> mainQueue = new ConcurrentQueue<XMLFile>();
            string xsltString = File.ReadAllText(_config.XSLTFilePath);

            var backgroundTasks = new[]
                  {
                    Task.Run(() => _readFile.ReadFileAndPutToQueueAsync(mainQueue)),
                    Task.Run(() => _convertFile.GetDataAndConvert(mainQueue,xsltString))
                };

            await Task.WhenAll(backgroundTasks);

            Console.ReadLine();
        }
    }
}
