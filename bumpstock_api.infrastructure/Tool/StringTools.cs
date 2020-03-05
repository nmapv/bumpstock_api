using Newtonsoft.Json.Linq;
using System;
using System.Text;
using System.Text.RegularExpressions;

namespace bumpstock_api.infrastructure.Tool
{
    public static class StringTools
    {
        public static string FormatMaxLength(this string param, int length)
        {
            if (string.IsNullOrEmpty(param))
                return param;

            return param.Length <= length ? param : param.Substring(0, length);
        }

        public static string FormatCpf(this string param)
        {
            if (string.IsNullOrEmpty(param))
                return param;

            return Convert.ToUInt64(param).ToString(@"000\.000\.000\-00");
        }

        public static string FormatCnpj(this string param)
        {
            if (string.IsNullOrEmpty(param))
                return param;

            return Convert.ToUInt64(param).ToString(@"00\.000\.000\/0000\-00");
        }

        public static string FormatPhoneNumber(this string param)
        {
            if (string.IsNullOrEmpty(param))
                return param;

            if (param.Length == 10)
                return string.Format("({0}) {1}-{2}", param.Substring(0, 2), param.Substring(2, 4), param.Substring(6, 4));

            if (param.Length == 11)
                return string.Format("({0}) {1}-{2}", param.Substring(0, 2), param.Substring(2, 5), param.Substring(7, 4));

            return param;
        }

        public static string ReplaceRegex(this string param, string regexPattern, string replacement)
        {
            if (string.IsNullOrEmpty(param))
                return param;

            return new Regex(regexPattern).Replace(param, replacement);
        }

        public static string EncondeTo64(this string param)
        {
            byte[] toEncodeAsBytes = Encoding.ASCII.GetBytes(param);
            return Convert.ToBase64String(toEncodeAsBytes);
        }

        public static bool IsValidJson(this string param)
        {
            if (string.IsNullOrEmpty(param))
                return false;

            param = param.Trim();
            if (!((param.StartsWith("{") && param.EndsWith("}")) || (param.StartsWith("[") && param.EndsWith("]"))))
                return false;

            try
            {
                var obj = JToken.Parse(param);
                return true;
            }
            catch
            {

                return false;
            }
        }
    }
}
