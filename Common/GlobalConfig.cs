using System;

namespace Common
{
    public static class GlobalConfig
    {
        public static string IdentityserverUrl { get { return "http://10.0.4.49:6999"; } }

        public static string ConsulserverUrl { get { return "http://10.0.4.49:8500"; } }

        public static string Host { get { return "10.0.4.49"; } }
    }
}
