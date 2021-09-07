using EPRO.Library.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EWebFramework.Vendor.PageHandlers;

namespace EWebFramework.Vendor.mailing.mailable
{
    /// <summary>
    /// Mailable Abstract Class
    /// </summary>
    public abstract class Mail : IMailable
    {
        /// <summary>
        /// Location of the HTM file to use
        /// </summary>
        public abstract string ResourceHTMLFile { get; }
        /// <summary>
        /// Email address or addresses separated with comma
        /// </summary>
        public string Receiver { get; set; }
        /// <summary>
        /// Same as ToString(), it returns the generated HTML
        /// </summary>
        public string Message { get; private set; }
        /// <summary>
        /// Subject of the message
        /// </summary>
        public  string Subject { get; set; }
        /// <summary>
        /// List of files full path to add
        /// </summary>
        public abstract IEnumerable<string> FileFullPathNames { get; }
        /// <summary>
        /// Carbon copy list
        /// </summary>
        public  string CC { get; set; }
        /// <summary>
        /// Blind Carbon Copy list
        /// </summary>
        public  string BCC { get; set; }

        /// <summary>
        /// Creates class and generates default HTML body, throws exception if file is not found
        /// </summary>
        public Mail()
        {
            this.Subject = "SCADWARE";
            this.SetFinalHTML();
        }




        /// <summary>
        /// Generates the HTML Body
        /// </summary>
        /// <param name="p"></param>
        protected void SetFinalHTML(object p=null)
        {
            this.Message = this.Resource;
            if (p != null)
            {
                foreach (var r in p.GetType().GetFields())
                {
                    this.Message = this.Message.Replace(Variable(r.Name), EStrings.valueOf(r.GetValue(p)));
                }
                foreach (var r in p.GetType().GetProperties())
                {
                    this.Message = this.Message.Replace(Variable(r.Name), EStrings.valueOf(r.GetValue(p)));
                }
            }
        }



        private String Resource
        {
            get
            {
                String R = RootPath("mails/" + this.ResourceHTMLFile);
                if (!File.Exists(R)) throw new FileNotFoundException("Unable to locate resource file for mail in " + R);


                return File.ReadAllText(R);

            }
        }

     
        /// <summary>
        /// Sets HTML Variable with double curly brackets {{ }}
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        private String Variable(String v)
        {
            return "{{" + v + "}}";
        }


        /// <summary>
        /// Returns the Message body
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Message;
        }

    }
}
