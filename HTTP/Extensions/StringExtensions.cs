using System;
using System.Globalization;

namespace HTTP.Extensions
{
    public static class StringExtensions
    {
        public static string Capitalize(this string str)
        {
            //var textInfo = new CultureInfo("en-US", false).TextInfo;
            //string titledString = textInfo.ToTitleCase(str.ToLower());
            //return titledString;

            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentException($"{nameof(str)} cennot be null");
            }
            return char.ToUpper(str[0]) + str.Substring(1).ToLower();
        }
    }
}