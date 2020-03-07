namespace LoggerInitializer
{
    using System.Collections.Generic;

    public class ContextInformation
    {
        public string Host { get; set; }
        public string Method { get; set; }
        public string RemoteIpAddress { get; set; }
        public string Protocol { get; set; }
    }
}
