using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Converter.Settings
{
    public class ConverterConfig
    {
        public string InputFolder { get; set; }
        public string OutputFolder { get; set; }
        public string XSLTFilePath { get; set; }
    }
}

