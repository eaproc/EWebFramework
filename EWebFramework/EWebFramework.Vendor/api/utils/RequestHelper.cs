using EPRO.Library;
using EPRO.Library.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using static EWebFramework.Vendor.PageHandlers;

namespace EWebFramework.Vendor.api.utils
{
    /// <summary>
    /// Request Helper helps to get request posted
    /// </summary>
    public class RequestHelper:IJsonable
    {


        //
        //
        //  Treat variable as null if it is empty string if nullable,
        //  Unless you send it as JSON
        //
        //



        /// <summary>
        /// The variables sent
        /// </summary>
        public Dictionary<string, object> requestVariables;


       
        /// <summary>
        /// The string body of the post as string
        /// </summary>
        public string RequestBodyContent
        {
            get
            {
                try
                {
                    Stream req = this.OriginalRequest.InputStream;
                    req.Seek(0, System.IO.SeekOrigin.Begin);
                    // Read it without closing the stream. Because once the stream is closed, it is gone.
                    // 
                    return new StreamReader(req, Encoding.UTF8, true, 1024, true).ReadToEnd();
                }
                catch (Exception ex)
                {
                    Logger.Print("Error reading Request.Input String of Header: " + this.OriginalRequest.ContentType, ex);
                }

                return string.Empty;
            }
        }


        /// <summary>
        /// Current HttpRequest Object
        /// </summary>
        private HttpRequest OriginalRequest { set; get; }

        private readonly bool IsJsonRequest;


        /// <summary>
        /// Create an help to access post object
        /// </summary>
        /// <param name="request"></param>
        public RequestHelper(HttpRequest request)

        {

            try
            {
                this.requestVariables = new Dictionary<string, object>();
                this.OriginalRequest = request;
                this.IsJsonRequest = (request.ContentType == "application/json");
                foreach (var p in request.Params)
                {
                    if (p == null)
                    {
                        // Parameter is null but there is value.
                        // That occurs when you send like &value
                        // Instead of &value=something or atleast with =
                        // or like this $value=
                        //Logger.Print("p is null");
                        // We don't accept that parameter so it won't be regarded
                        continue;
                    }
                    RecursiveAddJson(new KeyValuePair<string, object>(p.ToString(), request[p.ToString()]));
                }



                // Only supports object not array, with array, this lines will crash
                if (this.IsJsonRequest)
                {
                    string content = RequestBodyContent;
                    if ( content != null && content.Trim().StartsWith("{"))
                    {
                        var d = JsonDeserializer.deserializeToDictionary(content);
                        if (d != null)
                        {
                            foreach (KeyValuePair<String, Object> p in d)
                                RecursiveAddJson(p);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


        private void RecursiveAddJson(KeyValuePair<String, Object> p, String AppendKeyName = "")
        {
            if (p.Value != null && p.Value.GetType() == typeof(Dictionary<String, Object>))
                    foreach (KeyValuePair<String, Object> pChild in (Dictionary<String, Object>)p.Value)
                        RecursiveAddJson(pChild, AppendKeyName + p.Key + ".");
                else
                {
                    if(!this.requestVariables.ContainsKey(p.Key))
                        this.requestVariables.Add(AppendKeyName + p.Key, p.Value);
                }
        }


     
        /// <summary>
        /// Checks if a key exists in the request sent
        /// </summary>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public bool ContainsKey(string paramName)
        {
            return this.requestVariables.ContainsKey(paramName);
        }


        /// <summary>
        /// Get an object
        /// </summary>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public object Get(string paramName)
        {
            //return this.requestVariables[paramName];
            return this.Get(paramName, true);
        }

        /// <summary>
        /// Only downside here is, if the request param is nullable and the value sent is "null", it will be seen as null
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="pIsNullable"></param>
        /// <returns></returns>
        public object Get(string paramName, bool pIsNullable)
        {
            if (!this.requestVariables.ContainsKey(paramName)) throw new KeyNotFoundException(String.Format("The parameter name [ {0} ] is not found!", paramName));

            if (this.IsJsonRequest) return this.requestVariables[paramName];


            /***
            * Solution here is make the string non-nullable for formdata and send empty string
            * or make it nullable if you are sure user aren't suppose to enter "null" for the parameter
            **/
            String s = EStrings.valueOf(this.requestVariables[paramName]);
            if (pIsNullable && IsQueryStringNullDefinition(s) ) s = null;

            return s;
        }


        /// <summary>
        /// Get unprocessed raw value of a parameter
        /// </summary>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public object GetOriginalSentValueOf(string paramName)
        {
            if (!this.requestVariables.ContainsKey(paramName)) throw new KeyNotFoundException(String.Format("The parameter name [ {0} ] is not found!", paramName));

            return this.requestVariables[paramName];
        }



        /// <summary>
        /// Checks if a file exists in the post
        /// </summary>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public bool HasFile(string paramName)
        {
            return this.OriginalRequest.Files.Keys.Cast<String>().Contains(paramName);
        }


        /// <summary>
        /// Return Content Length in Bytes
        /// </summary>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public int FileSize(string paramName)
        {
            return this.OriginalRequest.Files[paramName].ContentLength;
        }



    /// <summary>
    /// It is only true if the object is string and the value=null or undefined
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
        public static bool IsQueryStringNullDefinition(object v)
        {
                if (v == null || !(v is string)) return false;
            return (v.ToString() == "null" || v.ToString() == "undefined");


        }








        /// <summary>
        /// Returns headers as object but preferrable if you access as dictionary
        /// ExpandoObject
        /// </summary>
        /// <returns></returns>
        public dynamic HeadersObject()
        {

            dynamic pairs = new System.Dynamic.ExpandoObject();

            var x = pairs as IDictionary<string, Object>;

            foreach (var v in this.OriginalRequest.Headers.AllKeys)
                x.Add(v, this.OriginalRequest.Headers[v]);

            return pairs;

        }

        /// <summary>
        /// Returns headers as JSON
        /// </summary>
        /// <returns></returns>
        public string HeadersJson()
        {
            return JsonConvert.SerializeObject(this.HeadersObject());
        }






        /// <summary>
        /// Return Variables as Object
        /// ExpandoObject == > Dictionary
        /// </summary>
        /// <returns></returns>
        public dynamic ToObject()
        {

            dynamic pairs = new System.Dynamic.ExpandoObject();

            var x = pairs as IDictionary<string, Object>;

            foreach (var v in this.requestVariables)
                x.Add(v);

            return pairs;

        }



        /// <summary>
        /// Returns as JSON
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this.ToObject());
        }


    }
}