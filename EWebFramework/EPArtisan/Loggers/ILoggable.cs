using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPArtisan.Loggers
{
    public interface ILoggable
    {
        void Write(String contents);
    }
}
