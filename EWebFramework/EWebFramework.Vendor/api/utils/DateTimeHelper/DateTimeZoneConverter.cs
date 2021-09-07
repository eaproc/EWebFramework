using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EWebFramework.Vendor.api.utils.DateTimeHelper
{
    public static class DateTimeZoneConverter
    {
        public static DateTime ToTimezone(this DateTime srcDateTime , 
            String srcTimeZone, String dstTimeZone)
        {
            return ToTimezone(srcDateTime: srcDateTime,
                srcTimeZone: new TimeZoneManager(srcTimeZone),
                dstTimeZone: new TimeZoneManager(dstTimeZone)
                );
        }

        public static DateTime ToTimezone(this DateTime srcDateTime,
            TimeZoneManager srcTimeZone, TimeZoneManager dstTimeZone)
        {
            DateTime utcTime = srcDateTime.AddMinutes(- srcTimeZone.getOffsetInMinutes());
            return utcTime.AddMinutes(dstTimeZone.getOffsetInMinutes());
        }
    }
}