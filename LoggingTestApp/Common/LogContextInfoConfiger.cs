namespace LoggingTestApp.Common
{
    using System;
    using Microsoft.AspNetCore.Http;

    // Provides HttpRequest Info 
    public static class LogContextInfoConfiger
    {
        // Use: the method should be attached to a string message or the exeption
        // Example: Log.Error(new Exception("Buuuuuuuum") + LogContextInfoConfiger.logCtxInfo(Request));
        // Example: Log.Error("Some message here!" + LogContextInfoConfiger.logCtxInfo(Request));

        public static string logCtxInfo(HttpRequest request)
        {
            string remoteIpAddress = "IP: " + request
                .HttpContext
                .Request
                .HttpContext
                .Connection
                .RemoteIpAddress
                .ToString();

            string host = "Host: " + request
                .Host
                .ToString();

            string method = "Method: " + request.Method;
            string protocol = "Protocol: " + request.Protocol;
            var newLine = Environment.NewLine;

            return newLine +
                remoteIpAddress +
                newLine +
                host +
                newLine +
                method +
                newLine +
                protocol +
                newLine;
        }
    }
}
