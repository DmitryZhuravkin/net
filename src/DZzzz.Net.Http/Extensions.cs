using System;
using System.ComponentModel;
using System.Reflection;

namespace DZzzz.Net.Http
{
    public static class Extensions
    {
        public static Uri CombineAndConvertToUri(this string absolute, string relative)
        {
            string temp = absolute;

            if (!temp.EndsWith("/"))
            {
                temp = temp + "/";
            }

            Uri baseUri = new Uri(temp, UriKind.Absolute);

            return new Uri(baseUri, relative);
        }

        public static string GetDescription<T>(this T source)
        {
            FieldInfo fi = source.GetType().GetRuntimeField(source.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }

            return source.ToString();
        }
    }
}