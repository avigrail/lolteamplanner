using System.Collections.Generic;

namespace TeamPicks
{
    public class PlayerData
    {
        public string Name { get; set; }
        public List<string> MainPicks { get; set; } = new List<string>();
        public List<string> AlternativePicks { get; set; } = new List<string>();
        public List<string> OtherPicks { get; set; } = new List<string>();
    }
}