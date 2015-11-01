namespace ZE1Sharp
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Text;

    public static class UriExtensions
    {
        public static Uri AttachParameters(this Uri uri, IDictionary<string, string> parameters)
        {
            if (parameters == null)
            {
                return uri;
            }

            var stringBuilder = new StringBuilder();
            var separator = "?";
            foreach (var parameter in parameters)
            {
                stringBuilder.Append(separator);
                stringBuilder.Append(Uri.EscapeUriString(parameter.Key));
                stringBuilder.Append("=");
                stringBuilder.Append(Uri.EscapeUriString(parameter.Value));
                separator = "&";
            }
            return new Uri(uri + stringBuilder.ToString());
        }

        public static string AttachParameters(this string path, IDictionary<string, string> parameters)
        {
            if (parameters == null)
            {
                return path;
            }

            var stringBuilder = new StringBuilder();
            var separator = "?";
            foreach (var parameter in parameters)
            {
                stringBuilder.Append(separator);
                stringBuilder.Append(Uri.EscapeUriString(parameter.Key));
                stringBuilder.Append("=");
                stringBuilder.Append(Uri.EscapeUriString(parameter.Value));
                separator = "&";
            }

            return path + stringBuilder;
        }

        public static string AttachParameter(this string path, string key, string value)
        {
            var stringBuilder = new StringBuilder();
            var separator = path.Contains("?") ? "&" : "?";
            stringBuilder.Append(separator);
            stringBuilder.Append(Uri.EscapeUriString(key));
            stringBuilder.Append("=");
            stringBuilder.Append(Uri.EscapeUriString(value));
            return path + stringBuilder;
        }
    }
}