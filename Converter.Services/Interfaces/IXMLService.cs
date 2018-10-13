using Converter.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Converter.Services.Interfaces
{
    public interface IXMLService
    {
        IList<XMLFile> ReadXMLFiles(string folderPath);
    }
}
