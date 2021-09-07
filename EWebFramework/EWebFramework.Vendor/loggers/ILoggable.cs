using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EWebFramework.Vendor.loggers
{
    public interface ILoggable
    {
        void Write(String contents);
    }
}
