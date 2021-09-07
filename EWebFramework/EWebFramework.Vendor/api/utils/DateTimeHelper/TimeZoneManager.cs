using EPRO.Library.InputsParsing;
using EPRO.Library.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EWebFramework.Vendor.api.utils.DateTimeHelper
{
    public class TimeZoneManager
    {

        private String Sign;
        private int Hour;
        private int Min;

        private const int HOUR_PART_MAX = 23;
        private const int MINUTE_PART_MAX = 59;


        public TimeZoneManager(String timeZone)
        {
            if (timeZone == null || timeZone == String.Empty) throw new InvalidTimeZoneSettingsException();

            String pSign = timeZone.Substring(0, 1);
            if (pSign != "+" && pSign != "-" && !TextParsing.IsNumber(pSign.First())) throw new InvalidTimeZoneSettingsException();
            if (pSign != "+" && pSign != "-")
                this.Sign = "+";
            else
                this.Sign = pSign;

            try
            {
                String valuePart = !TextParsing.IsNumber(pSign.First()) ? timeZone.Substring(1) : timeZone;
                String[] vParts = valuePart.Split(':');

                this.Hour = EInt.valueOf(vParts[0]);
                this.Min = EInt.valueOf(vParts[1]);

                if (this.Hour > HOUR_PART_MAX || this.Min > MINUTE_PART_MAX) throw new InvalidTimeZoneSettingsException();



            }
            catch (Exception)
            {
                throw new InvalidTimeZoneSettingsException();
            }


        }






        public int getSignMultiplier()
        {
            return this.Sign == "+" ? 1 : -1;
        }


        public int getOffsetInMinutes()
        {
            return ((this.Hour * 60) + this.Min) * this.getSignMultiplier();
        }


        public bool isOffsetPositive()
        {
            return this.getSignMultiplier() > 0;
        }



        public override string ToString()
        {
            return this.Sign + EStrings.WrapUp(this.Hour.ToString(), 2) +
                ":" + EStrings.WrapUp(this.Min.ToString(), 2); 
        }



    }
}