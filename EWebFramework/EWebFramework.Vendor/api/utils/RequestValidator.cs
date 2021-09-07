using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using EPRO.Library.InputsParsing;
using EPRO.Library.Objects;
using EPRO.Library;
using EWebFramework.Vendor.api.services.DataTablesNET;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using static EWebFramework.Vendor.PageHandlers;

namespace EWebFramework.Vendor.api.utils
{
    public class RequestValidator
    {

        public RequestValidator()
        {
            this.errors = new SortedList<string, string>();
            this.ValidatedRules = new Dictionary<string, Rule>();
        }



        public SortedList<string, string> errors;
        public struct Rule
        {
            public string paramName;
            public bool isRequired;
            public long paramMinSize;
            public long paramMaxSize;
            public ParamTypes paramType;
            public bool IsNullable;

            public enum ParamTypes
            {
                STRING, UNESCAPED_STRING, NUMERIC_STRING,  EMAIL,
                FILE, INTEGER, DECIMAL, BOOLEAN, JSON, DATE, DATE_TIME, TIME
            }

            public Rule(string pName, bool pIsRequired, long pParamMinSize = 1, long pParamMaxSize = 50, ParamTypes pParamType = ParamTypes.STRING, bool pIsNullable = false)
            {
                this.paramName = pName;
                this.isRequired = pIsRequired;
                this.paramMinSize = pParamMinSize;
                this.paramMaxSize = pParamMaxSize;
                this.paramType = pParamType;
                this.IsNullable = pIsNullable;
            }

        }


        private readonly Dictionary<String, Rule> ValidatedRules;



        private static bool IsValidJson(string strInput)
        {
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    //Exception in parsing json
                    Console.WriteLine(jex.Message);
                    return false;
                }
                catch (Exception ex) //some other exception
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }
        }



        private void Check(RequestHelper requestHelper, HttpResponse response, System.Web.SessionState.HttpSessionState session, Rule r)
        {
            // Logger.Print(r.paramName);
            if (r.isRequired && (
                (
                    !requestHelper.ContainsKey(r.paramName)
                    ||
                    (requestHelper.ContainsKey(r.paramName) && EStrings.valueOf(requestHelper.Get(r.paramName)) == String.Empty)
                )
                && !requestHelper.HasFile(r.paramName)
                )
                )
            {
                this.errors.Add(r.paramName, r.paramName.Replace("_", " ") + " is required must be of type " + r.paramType.ToString());
                return;
            }


            //
            // Test for NULL
            //
            if (requestHelper.ContainsKey(r.paramName))
            {
                var t = EStrings.valueOf(requestHelper.Get(r.paramName));

                if ((t == "null" || t == string.Empty) && r.IsNullable) return;

            }




            switch (r.paramType)
            {
                case Rule.ParamTypes.UNESCAPED_STRING:
                case Rule.ParamTypes.STRING:
                    if (requestHelper.ContainsKey(r.paramName))
                    {
                        var s = EStrings.valueOf(requestHelper.Get(r.paramName));
                        if(r.paramType== Rule.ParamTypes.UNESCAPED_STRING) s = Uri.UnescapeDataString(s);

                        if (s.Length > r.paramMaxSize || s.Length < r.paramMinSize)
                            this.errors.Add(r.paramName, string.Format("The size of  {0} must be minimum of {1} and maximum of {2}",
                                r.paramName, r.paramMinSize, r.paramMaxSize)
                                );

                        return;
                    }
                    break;
                case Rule.ParamTypes.EMAIL:
                    if (requestHelper.ContainsKey(r.paramName))
                    {
                        var s = EStrings.valueOf(requestHelper.Get(r.paramName));
                        if (s.Length > r.paramMaxSize || s.Length < r.paramMinSize)
                            this.errors.Add(r.paramName, string.Format("The size of  {0} must be minimum of {1} and maximum of {2}",
                                r.paramName, r.paramMinSize, r.paramMaxSize)
                                );
                        
                        if (!TextParsing.IsValidEmail( s) )
                            this.errors.Add(r.paramName, string.Format("The value of {0} must be a valid Email Address", r.paramName));
                        return;
                    }
                    break;
                case Rule.ParamTypes.NUMERIC_STRING:
                    // 
                    // Acts as string but makes sure the content is all number
                    //
                    if (requestHelper.ContainsKey(r.paramName))
                    {
                        var s = EStrings.valueOf(requestHelper.Get(r.paramName));
                        if (s.Length > r.paramMaxSize || s.Length < r.paramMinSize)
                            this.errors.Add(r.paramName, string.Format("The size of  {0} must be minimum of {1} and maximum of {2}",
                                r.paramName, r.paramMinSize, r.paramMaxSize)
                                );

                        if (s.Where(x=> !TextParsing.IsNumber(x)).Count()>0)
                            this.errors.Add(r.paramName, string.Format("The content  {0} must be numbers only [0 to 9]", r.paramName) );

                        return;
                    }
                    break;
                case Rule.ParamTypes.BOOLEAN:
                    if (requestHelper.ContainsKey(r.paramName))
                    {
                        var s = EStrings.valueOf(requestHelper.Get(r.paramName));
                        if (s.Length == 0 ||
                            (s != "1" && s != "0" && s.ToLower() != "true" && s.ToLower() != "false")
                            )
                        {
                            this.errors.Add(r.paramName, string.Format("Invalid value passed in for {0}. It must be a [1 or 0] or [true or false].", r.paramName)
                                );
                            return;
                        }

                    }

                    break;
                case Rule.ParamTypes.DECIMAL:
                    if (requestHelper.ContainsKey(r.paramName))
                    {
                        var s = EStrings.valueOf(requestHelper.Get(r.paramName));

                        if ((s == "null" || s == string.Empty) && r.IsNullable) return;

                        decimal p = 0;

                        if (
                            (r.paramMinSize != 0 && (s.Length == 0 || !Decimal.TryParse(s, out p))))
                        {
                            this.errors.Add(r.paramName, string.Format("Invalid value passed in for {0}. It must be a valid decimal.", r.paramName)
                                );
                            return;
                        }
                        //decimal p = EDecimal.valueOf(s);
                        if (p < r.paramMinSize || p > r.paramMaxSize)
                        {
                            this.errors.Add(r.paramName, string.Format("The size of  {0} must be minimum of {1} and maximum of {2}",
                              r.paramName, r.paramMinSize, r.paramMaxSize)
                              );
                        }

                    }
                    break;
                case Rule.ParamTypes.FILE:
                    if (requestHelper.ContainsKey(r.paramName) && requestHelper.Get(r.paramName) != null && EStrings.valueOf(requestHelper.Get(r.paramName)) != string.Empty)
                    {
                        Logger.Print(EStrings.valueOf(requestHelper.Get(r.paramName)));
                        this.errors.Add(r.paramName, string.Format("Invalid value passed in for {0}. It must be file.", r.paramName)
                               );
                        return;
                    }
                    if (requestHelper.HasFile(r.paramName) && requestHelper.FileSize(r.paramName) > (r.paramMaxSize * 1024))
                    {
                        this.errors.Add(r.paramName, string.Format("The size of  {0} must be maximum of {2}kb. You uploaded {1}!",
                            r.paramName, EMaths.getReadableByteValue(requestHelper.FileSize(r.paramName)), r.paramMaxSize)
                            );
                    }


                    break;
                case Rule.ParamTypes.INTEGER:
                    if (requestHelper.ContainsKey(r.paramName))
                    {
                        var s = EStrings.valueOf(requestHelper.Get(r.paramName));

                        if (
                            (r.paramMinSize != 0 && s.Length == 0) || TextParsing.parseOutIntegers(s).Length != s.Length)
                        {
                            this.errors.Add(r.paramName, string.Format("Invalid value passed in for {0}. It must be a valid Integer.", r.paramName)
                                );
                            return;
                        }
                        int p = EInt.valueOf(s);
                        if (p < r.paramMinSize || p > r.paramMaxSize)
                        {
                            this.errors.Add(r.paramName, string.Format("The size of  {0} must be minimum of {1} and maximum of {2}",
                              r.paramName, r.paramMinSize, r.paramMaxSize)
                              );
                        }

                    }

                    break;


                case Rule.ParamTypes.JSON:
                    if (requestHelper.ContainsKey(r.paramName))
                    {
                        var s = EStrings.valueOf(requestHelper.Get(r.paramName));
                        if (!IsValidJson(s))
                            this.errors.Add(r.paramName, string.Format("The value of  {0} must be a valid json!", r.paramName)
                                );

                        return;
                    }
                    break;


                case Rule.ParamTypes.DATE:
                    if (requestHelper.ContainsKey(r.paramName))
                    {
                        var s = EStrings.valueOf(requestHelper.Get(r.paramName));
                        if (DataTableRequestFields.ParseDate(s) == null)
                        {
                            this.errors.Add(r.paramName, string.Format("Invalid value passed in for {0}. It must be a valid date with format (yyyy-MM-dd).", r.paramName)
                                );
                            return;
                        }

                    }

                    break;

                case Rule.ParamTypes.TIME:
                    if (requestHelper.ContainsKey(r.paramName))
                    {
                        var s = EStrings.valueOf(requestHelper.Get(r.paramName));
                        if (DataTableRequestFields.ParseTime(s) == null)
                        {
                            this.errors.Add(r.paramName, string.Format("Invalid value passed in for {0}. It must be a valid time with format ({1}).",
                                r.paramName, DataTableRequestFields.TIME_FORMAT)
                                );
                            return;
                        }

                    }
                    break;


            }


        }




        public bool Validate(RequestHelper requestHelper, HttpResponse response, System.Web.SessionState.HttpSessionState session,
            params Rule[] rules)
        {

            this.errors.Clear();


            foreach (Rule r in rules)
            {
                Check(requestHelper, response, session, r);
                this.ValidatedRules.Add(r.paramName, r);
            }

            return this.errors.Count == 0;
        }





        /// <summary>
        /// it checks if it is APP_DEBUG mode, it shows detail error in English else just simple error in Selected language
        /// </summary>
        /// <returns></returns>
        public Dictionary<String, String> OutputErrors()
        {
            if (EBoolean.valueOf(ENV.General("APP_DEBUG")))
                return this.errors.ToDictionary(x => x.Key, x => x.Value);

            Dictionary<String, String> r = new Dictionary<string, string>
            {
                { "error", "Fill the required fields!" }
            };

            return r;

        }







        public T GetValue<T>(String pParamName){
            if(!this.ValidatedRules.ContainsKey(key: pParamName)) return default(T);

            Rule r = this.ValidatedRules[key: pParamName];
            RequestHelper requestHelper = new RequestHelper(HttpContext.Current.Request);

            if (!requestHelper.ContainsKey(pParamName) && !requestHelper.HasFile(paramName: pParamName) ) return default(T);


            String s = r.paramType != Rule.ParamTypes.FILE ?  EStrings.valueOf(requestHelper.Get(pParamName)) : null;

            switch (r.paramType)
            {
                case Rule.ParamTypes.EMAIL:
                case Rule.ParamTypes.UNESCAPED_STRING:
                case Rule.ParamTypes.STRING:
                    if (r.IsNullable && s == String.Empty)
                    {
                        //it is empty string not null
                        if (!RequestHelper.IsQueryStringNullDefinition(requestHelper.GetOriginalSentValueOf(pParamName)))
                            return (T)(object)String.Empty;
                        
                        //it is null
                        return (T)(object)(String)null;
                    }

                    if (r.paramType == Rule.ParamTypes.UNESCAPED_STRING) return (T)(object)Uri.UnescapeDataString(s);
                    return (T)(object)s;

                case Rule.ParamTypes.NUMERIC_STRING:
                    if (r.IsNullable && s == String.Empty) return (T)(object)(String)null;
                    return (T)(object)s;

                case Rule.ParamTypes.BOOLEAN:
                    if (r.IsNullable && s == String.Empty) return default(T);
                    return (T)(object)EBoolean.valueOf(s);

                case Rule.ParamTypes.DECIMAL:
                    if (r.IsNullable && s == String.Empty) return default(T);
                    return (T)(object)EDecimal.valueOf(s);

               
                case Rule.ParamTypes.INTEGER:
                    if (r.IsNullable && s == String.Empty) return default(T);
                    return (T)(object)EInt.valueOf(s);

                case Rule.ParamTypes.DATE:
                    if (r.IsNullable && s == String.Empty) return default(T);
                    return (T)(object)DataTableRequestFields.ParseDate(s).Value;

                case Rule.ParamTypes.TIME:
                    if (r.IsNullable && s == String.Empty) return default(T);
                    return (T)(object)DataTableRequestFields.ParseTime(s).Value;

                case Rule.ParamTypes.JSON:
                    if (r.IsNullable && s == String.Empty) return default(T);
                    return (T)(object) s;

                case Rule.ParamTypes.FILE:
                    // it won't reach here if it is not nullable
                    if (r.IsNullable && !requestHelper.HasFile(paramName: pParamName)) return default(T);
                    return (T)(object)HttpContext.Current.Request.Files[pParamName];


            }


            throw new NotImplementedException("This type [" + r.paramType.ToString() + "] is not yet supported!");

        }


    }
}