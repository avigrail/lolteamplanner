using Newtonsoft.Json;
using System.Collections.Generic;

namespace TeamTierList.Core
{
    public static class SerializationService
    {
        public static string Serialize(MainViewModel mainViewModel)
        {
            var dataObject = new TierListDataObject();
            dataObject.ShowChampionPool = mainViewModel.ShowChampionPool;
            dataObject.UseTileView = mainViewModel.UseTileView;
            dataObject.PlayerData = new List<PlayerDataObject>();

            foreach (var player in mainViewModel.Players)
            {
                var playerData = new PlayerDataObject();
                playerData.Name = player.Name;
                playerData.IsExpanded = player.IsExpanded;

                playerData.TierS = new List<string>();
                foreach (var champion in player.TierS)
                {
                    playerData.TierS.Add(champion.Name);
                }

                playerData.TierA = new List<string>();
                foreach (var champion in player.TierA)
                {
                    playerData.TierA.Add(champion.Name);
                }

                playerData.TierB = new List<string>();
                foreach (var champion in player.TierB)
                {
                    playerData.TierB.Add(champion.Name);
                }

                dataObject.PlayerData.Add(playerData);
            }

            return JsonConvert.SerializeObject(dataObject);
        }

        public static void Deserialize(string dataFromFile, MainViewModel mainViewModel)
        {
            var dataObject = JsonConvert.DeserializeObject<TierListDataObject>(dataFromFile);

            mainViewModel.ShowChampionPool = dataObject.ShowChampionPool;
            mainViewModel.UseTileView = dataObject.UseTileView;

            foreach (var playerData in dataObject.PlayerData)
            {
                var player = new Player();
                player.Name = playerData.Name;
                player.IsExpanded = playerData.IsExpanded;

                foreach (var championName in playerData.TierS)
                {
                    var existingChampion = mainViewModel.Champions.TrySingleOrDefault(c => c.Name == championName);
                    if (existingChampion != null)
                    {
                        player.TierS.Add(existingChampion);
                    }
                }

                foreach (var championName in playerData.TierA)
                {
                    var existingChampion = mainViewModel.Champions.TrySingleOrDefault(c => c.Name == championName);
                    if (existingChampion != null)
                    {
                        player.TierA.Add(existingChampion);
                    }
                }

                foreach (var championName in playerData.TierB)
                {
                    var existingChampion = mainViewModel.Champions.TrySingleOrDefault(c => c.Name == championName);
                    if (existingChampion != null)
                    {
                        player.TierB.Add(existingChampion);
                    }
                }

                mainViewModel.Players.Add(player);
            }
        }
    }
}
