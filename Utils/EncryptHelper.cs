using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace VideoPay.Utils
{
    public class EncryptHelper
    {
        public static string MD5Encypt(string value, string format = "x2")
        {
            using (var md5 = MD5.Create())
            {
                var bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(value));
                StringBuilder sb = new StringBuilder();
                foreach (var item in bytes)
                {
                    sb.Append(item.ToString(format));
                }
                return sb.ToString();
            }
        }
    }
}