using EPRO.Library.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using static EWebFramework.Vendor.PageHandlers;

namespace EWebFramework.Vendor.api.utils
{
    public class SessionHelper:IArrayable,IJsonable
    {

        public int? userId;
        /// <summary>
        /// this indicates when the session is new, mostly after it expires (timeout) or abandoned and 
        /// no variable is set in the session
        /// </summary>
        public bool isNewSession;
        public bool isReadonly;

        /// <summary>
        /// mostly fix per browser and not regenerated on expiry for cookless
        /// </summary>
        public string sessionId;

        public int sessionTimeOutInMins;
        public DateTime lastActive;
        public string ipAdress;
        public string browser;
        public Dictionary<string, object> sessionVariables;

        public SessionHelper(HttpSessionState Session, HttpRequest request)
        {

            try
            {

                // Improving this to be used even without request
                if (Session != null)
                {
                    Object pUserId = Fetch("userId");
                    if (pUserId != null) this.userId = Convert.ToInt32(pUserId);


                    this.isNewSession = Session.IsNewSession;
                    this.isReadonly = Session.IsReadOnly;
                    this.sessionId = Session.SessionID;
                    this.sessionTimeOutInMins = Session.Timeout;
                }

                this.lastActive = DateTime.Now;

                if (request != null)
                {
                    this.ipAdress = GetIPAddress();

                    this.browser = GetBrowserAgentDetails(request);
                }

                if (Session != null)
                    this.sessionVariables = (
                        from l in Session.Keys.Cast<String>()
                        select new KeyValuePair<String, Object>(l, Session[l])
                        ).ToDictionary(x => x.Key, x => x.Value);

            }
            catch (Exception e)
            {
                Logger.Print(e);
            }

        }


        public SessionHelper(Dictionary<string, object> pValues)
        {

            try
            {

                this.userId = (int?)pValues["userId"];
                this.isNewSession = (bool)pValues["isNewSession"];
                this.isReadonly = (bool)pValues["isReadonly"];
                this.sessionId = (string)pValues["sessionId"];
                this.sessionTimeOutInMins = (int)pValues["sessionTimeOutInMins"];
                this.lastActive = (DateTime)pValues["lastActive"];
                this.ipAdress = (String)pValues["ipAdress"];
                this.browser = (String)pValues["browser"];
                this.sessionVariables = (System.Collections.Generic.Dictionary<string, object>)pValues["sessionVariables"];


            }
            catch (Exception e)
            {
                Logger.Print(JsonConvert.SerializeObject(pValues));
                Logger.Print(e);
            }



        }

        /// <summary>
        /// Be carefull, HttpContext.Current.Request Request throws exception on Current Property if not available instead of returning null
        /// </summary>
        public SessionHelper() : this(Session: HttpContext.Current.Session, request: HttpContext.Current.Request)
        {}







        /// <summary>
        /// Appends UserVariable. to the name of the variable
        /// </summary>
        /// <param name="VariableName"></param>
        /// <param name="Value"></param>
        public static void Put(String VariableName, Object Value)
        {
            HttpSessionState Session = System.Web.HttpContext.Current.Session;
            Session["UserVariable." + VariableName] = Value;
        }


        /// <summary>
        /// Appends UserVariable. to the name of the variable
        /// </summary>
        /// <param name="VariableName"></param>
        /// <returns></returns>
        public static Object Fetch(String VariableName)
        {
            HttpSessionState Session = System.Web.HttpContext.Current.Session;
            String pKey = "UserVariable." + VariableName;
            String[] pKeys = Session.Keys.Cast<String>().ToArray();
            if (pKeys.Contains(pKey))
                return  Session[pKey];

            return null;

        }


        /// <summary>
        /// You can restore by reseting the userID
        /// </summary>
        /// <param name="pUserID"></param>
        public static void StoreUserID(int pUserID)
        {
            Put("userId", pUserID);
        }





        /// <summary>
        /// Allows you to store strong typed object in the session. Still stores with appeding UserVariable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="varName"></param>
        /// <returns></returns>
        public static T GetStrongTypedObject<T>(string varName) where T: new()
        {
            String v = EStrings.valueOf(Fetch(varName));
            return JsonDeserializer.DeserializeToObjectType<T>(v);
        }

        /// <summary>
        /// Allows you to store strong typed object in the session. Still stores with appeding UserVariable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="v"></param>
        /// <param name="varName"></param>
        public static void SetStrongTypedObject<T>(T v, string varName)
        {
            String z = null;
            if (v != null)
                z = new JavaScriptSerializer().Serialize(v);
            Put(varName, z);
        }




        //T GetObject<T>(Dictionary<string, object> dict)
        //{
        //    Type type = typeof(T);
        //    var obj = Activator.CreateInstance(type);

        //    foreach (var kv in dict)
        //    {
        //        if (type.GetProperty(kv.Key) != null)
        //            type.GetProperty(kv.Key).SetValue(obj, kv.Value);
        //        else if(type.GetField(kv.Key)!=null)
        //            type.GetField(kv.Key).SetValue(obj, kv.Value);

        //    }
        //    return (T)obj;
        //}




        protected string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }




        private String GetBrowserAgentDetails(HttpRequest request)
        {
            System.Web.HttpBrowserCapabilities browser = request.Browser;
            string s = "Browser Capabilities\n"
                + "Type = " + browser.Type + "\n"
                + "Name = " + browser.Browser + "\n"
                + "Version = " + browser.Version + "\n"
                + "Major Version = " + browser.MajorVersion + "\n"
                + "Minor Version = " + browser.MinorVersion + "\n"
                + "Platform = " + browser.Platform + "\n"
                + "Is Beta = " + browser.Beta + "\n"
                + "Is Crawler = " + browser.Crawler + "\n"
                + "Is AOL = " + browser.AOL + "\n"
                + "Is Win16 = " + browser.Win16 + "\n"
                + "Is Win32 = " + browser.Win32 + "\n"
                + "Supports Frames = " + browser.Frames + "\n"
                + "Supports Tables = " + browser.Tables + "\n"
                + "Supports Cookies = " + browser.Cookies + "\n"
                + "Supports VBScript = " + browser.VBScript + "\n"
                + "Supports JavaScript = " +
                    browser.EcmaScriptVersion.ToString() + "\n"
                + "Supports Java Applets = " + browser.JavaApplets + "\n"
                + "Supports ActiveX Controls = " + browser.ActiveXControls
                      + "\n"
                + "Supports JavaScript Version = " +
                    browser["JavaScriptVersion"] + "\n";

            return s;
        }






        /// <summary>
        /// This is used to restore all variables stored under "UserVariable." that was lost in last session
        /// </summary>
        /// <param name="storedSession"></param>
        /// <param name="currentSession"></param>
        public static void RestoreSessionVariables(SessionHelper storedSession, HttpSessionState currentSession)
        {
            var restorableKeys = (from l in storedSession.sessionVariables.Keys
                                  where (l.StartsWith("UserVariable."))
                                  select l);
            foreach (String pKey in restorableKeys)
            {
                if (storedSession.sessionVariables.ContainsKey(pKey))
                    currentSession[pKey] = storedSession.sessionVariables[pKey];
            }
        }





        public IEnumerable<object> toArray()
        {
            throw new NotImplementedException();
        }

        public string ToJson()
        {
            return new JavaScriptSerializer().Serialize(this);
        }







    }
}