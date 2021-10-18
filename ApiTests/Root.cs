using System.Collections.Generic;

namespace ApiTests
{
    public class Fiat
    {
        public string name { get; set; }
        public string symbol { get; set; }
    }

    public class Root
    {
        public string id { get; set; }
        public string name { get; set; }
        public double adjusted_volume_24h_share { get; set; }
        public List<Fiat> fiats { get; set; }
    }

    //Branch_A commit 3
}