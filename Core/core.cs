using System;
using System.Collections.Generic;

namespace StoreApp
{
    public static class Tools
    {
        public static Dictionary<string, string> GetAPIKeyHeader() => new Dictionary<string, string>
        {
            { "key", "yubrfe7834uibeihbjyfegew7i4k3ivugywef34gkvw87i3k4giw4" }
        };
    }

    public class ProspectRequest
    {
        public int id { get; set; }
        public string name { get; set; }
        public string company { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public DateTime date { get; set; }
        public string location { get; set; }
        public string logEntry { get; set; }
    }

    public class ProspectResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
    }

    public class ProductVersions
    {
        public string StoreApp { get; set; }
        public string Sync { get; set; }
        public string Backup { get; set; }
    }
}