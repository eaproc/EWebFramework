using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic;

namespace EWebFramework.Vendor.api.utils
{
    public class AlphaNumericCodeGenerator
    {
        static AlphaNumericCodeGenerator()
        {
            PoolOfSource = new List<char>();
            for (Int16 intC = 48; intC <= 57; intC++)
                PoolOfSource.Add((char)intC);

            for (Int16 intC = 65; intC <= 90; intC++)
                PoolOfSource.Add((char)intC);
        }





        private static List<char> PoolOfSource;




        public static string getCode()
        {
            // REM Shuffle
            Random r = new Random();
            PoolOfSource = PoolOfSource.OrderBy(x => PoolOfSource[r.Next(PoolOfSource.Count)]).ToList();

            return new string(PoolOfSource.ToArray());
        }






     

        /// <summary>
        ///         ''' Faster
        ///         ''' </summary>
        ///         ''' <param name="length"></param>
        ///         ''' <returns></returns>
        public static string RandomString(int length)
        {
               Random random = new Random(Guid.NewGuid().GetHashCode());
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }









    }
}
