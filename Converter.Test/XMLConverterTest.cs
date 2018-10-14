using Converter.Core.Extensions;
using Converter.Core.Models;
using Converter.Interfaces;
using Converter.Processors;
using Converter.Services.Interfaces;
using Converter.Services.Services;
using Converter.Settings;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Concurrent;
using System.IO;
using System.Reflection;

namespace Converter.Test
{
    [TestClass]
    public class XMLConverterTest
    {
        [TestMethod]
        public void TestGenerateHTMLFileName()
        {
            //mock a xml file Object
            XMLFile file = new XMLFile
            {
                Name = "Test.xml",
                FullPath = @"C:\Test.xml"
            };

            Assert.AreEqual(file.GetHTMLFileName(), "Test.html");
        }

        [TestMethod]
        public void TestReadXMLFiles()
        {
            string inputFolder = "DataSource";
            IXMLService service = new XMLService();
            var files = service.ReadXMLFiles(inputFolder);
            Assert.IsNotNull(files);
            Assert.AreEqual(files.Count, 2);
        }

        [TestMethod]
        public void TestReadFileProcessor()
        {
            ConverterConfig app = new ConverterConfig()
            {
                InputFolder = "DataSource",
                OutputFolder = "Data\\Output",
                XSLTFilePath = "Resources\\Computer.xslt"
            };

            var mock = new Mock<IOptions<ConverterConfig>>();
            mock.Setup(ap => ap.Value).Returns(app);

            IXMLService service = new XMLService();

            ConcurrentQueue<XMLFile> mainQueue = new ConcurrentQueue<XMLFile>();

            IReadFileProcessor readFileProcessor = new ReadFileProcessor(mock.Object, service);
            readFileProcessor.ReadFileAndPutToQueueAsync(mainQueue).Wait();

            Assert.AreEqual(mainQueue.Count, 2);

            //try to dequeue 1 object
            bool dequeue = mainQueue.TryDequeue(out XMLFile file);
            //check that we can get the object out of the queue
            Assert.IsTrue(dequeue);

            //check that we load the content of the xml file
            Assert.IsNotNull(file.Document);
        }

        [TestMethod]
        public void TestConvertFileProcessor()
        {
            ConverterConfig app = new ConverterConfig()
            {
                InputFolder = "DataSource",
                OutputFolder = "DataOutput",
                XSLTFilePath = "Resources\\Computer.xslt"
            };

            string xsltString = File.ReadAllText("Resources\\Computer.xslt");
            var mock = new Mock<IOptions<ConverterConfig>>();
            mock.Setup(ap => ap.Value).Returns(app);

            ConcurrentQueue<XMLFile> mainQueue = new ConcurrentQueue<XMLFile>();
            IXMLService service = new XMLService();
            IReadFileProcessor readFileProcessor = new ReadFileProcessor(mock.Object, service);
            readFileProcessor.ReadFileAndPutToQueueAsync(mainQueue).Wait();

            IConvertFileProcessor convertFileProcessor = new ConvertFileProcessor(mock.Object);
            convertFileProcessor.GetDataAndConvert(mainQueue, xsltString).Wait();

            DirectoryInfo di = new DirectoryInfo(app.OutputFolder);
            FileInfo[] files = di.GetFiles("*.html");

            Assert.AreEqual(files.Length, 2);

        }
    }
}
