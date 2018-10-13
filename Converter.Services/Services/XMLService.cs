using Converter.Core.Models;
using Converter.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Converter.Services.Services
{
    public class XMLService : IXMLService
    {
        public IList<XMLFile> ReadXMLFiles(string folderPath)
        {
            if (!Directory.Exists(folderPath))
                return null;
            IList<XMLFile> result = new List<XMLFile>();
            DirectoryInfo di = new DirectoryInfo(folderPath);
            FileInfo[] files = di.GetFiles("*.xml");
            foreach (var file in files)
            {
                XMLFile xML = new XMLFile
                {
                    Name = file.Name,
                    FullPath = file.FullName
                };
                result.Add(xML);
            }
            return result;
        }
    }
}
