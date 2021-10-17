using System.Collections.Generic;

namespace TeamTierList.Core
{
    public class PlayerDataObject
    {
        public bool IsExpanded { get; set; }
        public string Name { get; set; }
        public List<string> TierS { get; set; }
        public List<string> TierA { get; set; }
        public List<string> TierB { get; set; }     
    }
}