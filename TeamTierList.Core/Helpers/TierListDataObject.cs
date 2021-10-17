using System.Collections.Generic;

namespace TeamTierList.Core
{
    public class TierListDataObject
    {
        public bool UseTileView { get; set; }
        public bool ShowChampionPool { get; set; }
        public List<PlayerDataObject> PlayerData { get; set; }
    }
}