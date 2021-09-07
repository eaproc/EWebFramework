using EPArtisan.Loggers;
using EPArtisan.Utils;
using EPRO.Library;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace EPArtisan.Dumpers
{
    public static class BasicResponse
    {



        public static void OutputJSON(this ILoggable pPage, String json)
        {
            pPage.Write(JValue.Parse(json).ToString(Formatting.Indented));
        }

        public static void OutputJSON(this ILoggable pPage, IJsonable json)
        {
            OutputJSON(pPage, json.ToJson());
        }


    }
}