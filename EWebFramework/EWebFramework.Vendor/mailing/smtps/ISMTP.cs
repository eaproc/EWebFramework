using EWebFramework.Vendor.mailing.mailable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWebFramework.Vendor.mailing.smtps
{
   public interface ISMTP:IDisposable
    {

        String Name { get; }

        String Sender { get;  }

        String Host { get;  }


        int Port { get;  }

        String Username { get; }

        String Password { get; }


        bool Send(IMailable mailable);

        bool Send(IMailable mailable, String SenderEmailAddress);

        bool SendViaHTTP(IMailable mailable, String SenderEmailAddress);

        bool SendViaCustom(IMailable mailable, String SenderEmailAddress);


    }
}
