using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWebFramework.Vendor.mailing.mailable
{
    public interface IMailable
    {
        String ResourceHTMLFile { get; }
        /// <summary>
        /// Receivers email address separated with comma for multiple receivers
        /// </summary>
        String Receiver { get; set; }
        /// <summary>
        /// Carbon Copy email address separated with comma for multiple receivers. Will be visible to all receivers
        /// </summary>
        String CC { get; set; }
        /// <summary>
        /// Blind Carbon Copy email address separated with comma for multiple receivers. Will not be visible to any receiver
        /// </summary>
        String BCC { get; set; }
        String Message { get;  }
        String Subject { get;  }

        IEnumerable<String> FileFullPathNames { get;  }

    }
}
