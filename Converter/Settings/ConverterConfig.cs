using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Converter.Settings
{
    public class ConverterConfig
    {
        private string _inputFolder;
        public string InputFolder
        {
            get
            {
                if (!string.IsNullOrEmpty(_inputFolder))
                    return Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), _inputFolder);
                else
                    return _inputFolder;
            }
            set
            {
                _inputFolder = value;
            }
        }

        private string _outputFolder;
        public string OutputFolder
        {
            get
            {
                if (!string.IsNullOrEmpty(_outputFolder))
                    return Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), _outputFolder);
                else
                    return _outputFolder;
            }
            set
            {
                _outputFolder = value;
            }
        }

        private string _xSLTFilePath;
        public string XSLTFilePath
        {
            get
            {
                if (!string.IsNullOrEmpty(_xSLTFilePath))
                    return Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), _xSLTFilePath);
                else
                    return _xSLTFilePath;
            }
            set
            {
                _xSLTFilePath = value;
            }
        }
        public int ReadSpeed { get; set; }
        public int ConvertSpeed { get; set; }
    }
}
