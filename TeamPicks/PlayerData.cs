using System.Collections.Generic;

namespace TeamPicks
{
    public class PlayerData
    {
        public string Name { get; set; }
        public List<string> Mains { get; set; } = new List<string>();
        public List<string> Goods { get; set; } = new List<string>();
        public List<string> Reserves { get; set; } = new List<string>();
    }
}