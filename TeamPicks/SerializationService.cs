using Newtonsoft.Json;
using System.Collections.Generic;

namespace TeamPicks
{
    public static class SerializationService
    {
        public static string Serialize(List<Player> players)
        {
            var playerList = new List<PlayerData>();

            foreach (var player in players)
            {
                var playerData = new PlayerData();
                playerData.Name = player.Name;

                foreach (var champion in player.ChampionsMain)
                {
                    playerData.Mains.Add(champion.Name);
                }
                foreach (var champion in player.ChampionsGood)
                {
                    playerData.Goods.Add(champion.Name);
                }
                foreach (var champion in player.ChampionsReserve)
                {
                    playerData.Reserves.Add(champion.Name);
                }

                playerList.Add(playerData);
            }

            return JsonConvert.SerializeObject(playerList);
        }
    }
}
