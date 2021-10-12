using System;
using System.Collections;
using System.Collections.Generic;

namespace TeamPicks
{
    public class MainViewModel : ViewModelBase
    {

        public ICrawlerService Crawler { get; }

        public IList Champions => champions;
        private List<Champion> champions;

        public IList Players { get; } = new List<Player>();

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

        internal void ResetChamptionStates()
        {
            champions.ForEach(c => c.SetOpacity(1.0));
        }

        internal void ClearPlayerPicks()
        {
            foreach (Player player in Players)
            {
                player.ClearChampions.Execute(null);
            }
        }
    }
}