using EPRO.Library.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EWebFramework.Vendor.exceptions.database
{
    /// <summary>
    /// Indicates one of the numeric value to be filled is having an amount larger than its capacity
    /// </summary>
    public class TooLargeNumericValue : Exception
    {

        public TooLargeNumericValue(
            Exception exception = null) :base(message: "The update is containing large amount of figures, please, make sure all your values are at the defined range!",
            innerException: exception)
        {
        }





        public static void DetectAndThrow(Exception exception)
        {
            TooLargeNumericValue d = Detect(exception);
            if (d != null) throw d;
        }

        public static TooLargeNumericValue Detect(Exception exception)
        {
            String s = exception.Message;
            if (s.IndexOf("Arithmetic overflow error converting int to data type numeric") <0) return null;

            return new TooLargeNumericValue(exception: exception);

        }







    }
}