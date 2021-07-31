using System.Collections;

namespace TeamPicks
{
    public class MainViewModel : ViewModelBase
    {

        public ICrawlerService Crawler { get; }

        public IList Champions { get; set; }

        public IList Players { get; set; }

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
            Champions = Crawler.GetData();
            showChampionPool = true;
        }
    }
}