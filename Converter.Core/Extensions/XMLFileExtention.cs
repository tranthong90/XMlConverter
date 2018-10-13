using Converter.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Xsl;

namespace Converter.Core.Extensions
{
    public static class XMLFileExtention
    {
        public static string ConvertToHTML(this XMLFile xMLFile, string xsltString)
        {
            if (string.IsNullOrEmpty(xMLFile.Document))
                return null;

            using (StringReader srt = new StringReader(xsltString))
            using (StringReader sri = new StringReader(xMLFile.Document)) 
            {
                using (XmlReader xrt = XmlReader.Create(srt, new XmlReaderSettings { DtdProcessing = DtdProcessing.Parse }))
                using (XmlReader xri = XmlReader.Create(sri))
                {
                    XslCompiledTransform xslt = new XslCompiledTransform();
                    xslt.Load(xrt);
                    using (StringWriter sw = new StringWriter())
                    using (XmlWriter xwo = XmlWriter.Create(sw, xslt.OutputSettings)) // use OutputSettings of xsl, so it can be output as HTML
                    {
                        xslt.Transform(xri, xwo);
                        return sw.ToString();
                    }
                }
            }
        }

        public static async Task LoadDocumentAsync(this XMLFile xMLFile)
        {
            using (var reader = new StreamReader(xMLFile.FullPath))
            {
                xMLFile.Document = await reader.ReadToEndAsync();
            }
        }

        public static string GetHTMLFileName(this XMLFile xMLFile)
        {
            return Path.ChangeExtension(xMLFile.Name, ".html");
        }
    }
}
