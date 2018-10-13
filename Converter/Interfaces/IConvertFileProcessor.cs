using Converter.Core.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Converter.Interfaces
{
    public interface IConvertFileProcessor
    {
       
        Task GetDataAndConvert(ConcurrentQueue<XMLFile> queue,string xsltString);
    }
}
