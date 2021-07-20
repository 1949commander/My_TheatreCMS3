using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheatreCMS3.Helpers
{
    public class TextHelpers 
    {
        public static string WordLimit(string str, int limit) //method will limit number of characters that are displayed using ellipses
        {
            if (str.Length > limit)
            {
                if (str[limit].Equals(' '))
                {
                    str = str.Substring(0, limit) + "...";
                }
                else
                {
                    str = str.Substring(0, limit + 1) + "...";
                }
            }

            return str;
        }
    }
}