﻿using Converter.Core.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Converter.Interfaces
{
    public interface IReadFileProcessor
    {
        Task ReadFileAndPutToQueueAsync(ConcurrentQueue<XMLFile> queue);
    }
}
