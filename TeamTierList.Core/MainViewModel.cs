using System.Collections.Generic;

namespace TeamTierList.Core
{
    public class MainViewModel : ViewModelBase
    {
        public ICrawlerService Crawler { get; }

        public IEnumerable<Champion> Champions => champions;
        private List<Champion> champions;

        public List<Player> Players { get; } = new List<Player>();

        public bool ShowChampionPool
        {
            get => showChampionPool;
            set
            {
                showChampionPool = value;
                OnPropertyChanged();
            }
        }

        private bool showChampionPool;

        public bool UseTileView
        {
            get => useTileView;
            set
            {
                useTileView = value;
                OnPropertyChanged();
            }
        }

        private bool useTileView;

        public MainViewModel(ICrawlerService crawler)
        {
            Crawler = crawler;
        }

        public void Initialize()
        {
            champions = Crawler.GetData();
            showChampionPool = true;
        }

        public void ResetChamptionStates()
        {
            champions.ForEach(c => c.SetOpacity(1.0));
        }

        public void ClearPlayerPicks()
        {
            foreach (Player player in Players)
            {
                player.ClearChampions.Execute(null);
            }
        }
    }
}