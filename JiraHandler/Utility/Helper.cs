using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace JiraHandler.Utility
{
    public class Helper
    {
        public static string GetEncodedCredentials(string UserName, string Password)
        {
            return GetBase64String(string.Format($"{UserName}:{Password}"));
        }

        public static string GetBase64String(string value)
        {
            byte[] byteCredentials = UTF8Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(byteCredentials);
        }

    }
}