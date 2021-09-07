using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWebFramework.Vendor.api.utils
{
    interface IArrayable
    {
        IEnumerable<object> toArray();
    }
}
