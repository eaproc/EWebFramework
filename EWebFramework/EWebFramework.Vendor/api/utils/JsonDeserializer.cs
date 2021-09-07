using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using static EWebFramework.Vendor.PageHandlers;

namespace EWebFramework.Vendor.api.utils
{
    public class JsonDeserializer
    {


        /// <summary>
        /// returns null on failure
        /// </summary>
        /// <param name="pJson"></param>
        /// <returns></returns>
        public static Dictionary<String,Object> deserializeToDictionary(String pJson)
        {
            try
            {

                return new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(pJson);

            }catch(Exception e) { Logger.Print("ERROR DESERIALIZING: " + pJson, e); return null; }
        }


        /// <summary>
        /// returns null on failure
        /// </summary>
        /// <param name="pJson"></param>
        /// <returns></returns>
        public static T DeserializeToObjectType<T>(String pJson) where T: new()
        {
            try
            {
                if (pJson == null || pJson == string.Empty) return default(T);
                return new JavaScriptSerializer().Deserialize<T>(pJson);

            }
            catch (Exception e) { Logger.Log(e); return default(T); }
        }


    }
}