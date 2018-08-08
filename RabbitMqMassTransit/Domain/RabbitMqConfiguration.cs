using System;

namespace Domain
{
    public static class RabbitMqConfiguration
    {
        public static string RabbitMqUri { get; set; }
        public static string VirtualHost { get; set; }
        public static string FullAddress => string.Format("{0}/{1}/", RabbitMqUri, VirtualHost);
        public static string UserName { get; set; }
        public static string Password { get; set; }
        public static string OAuthServiceQueue { get; set; }
        public static TimeSpan Timeout { get; set; }
    }
}