using System;

namespace Common
{
    public static class GlobalConfig
    {
        public static string IdentityserverUrl { get { return "http://localhost:6999"; } }

        public static string ConsulserverUrl { get { return "http://localhost:8500"; } }

        public static string Host { get { return "localhost"; } }
    }
}
