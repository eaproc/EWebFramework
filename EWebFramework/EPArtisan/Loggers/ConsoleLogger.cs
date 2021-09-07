using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPArtisan.Loggers
{
    class ConsoleLogger : ILoggable
    {
        public void Write(string contents)
        {
            Console.WriteLine(contents);
        }
    }
}
