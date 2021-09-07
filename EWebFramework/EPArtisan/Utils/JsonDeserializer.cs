using EPArtisan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace EPArtisan.Utils
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

            }catch(Exception e) { Program.Logger.Log(e); return null; }
        }


        /// <summary>
        /// returns null on failure
        /// </summary>
        /// <param name="pJson"></param>
        /// <returns></returns>
        public static T deserializeToObjectType<T>(String pJson)
        {
            try
            {

                return new JavaScriptSerializer().Deserialize<T>(pJson);

            }
            catch (Exception e) { Program.Logger.Log(e); return default(T); }
        }


    }
}