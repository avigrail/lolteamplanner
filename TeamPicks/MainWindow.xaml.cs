using Newtonsoft.Json;
using System.ComponentModel;
using System.Windows;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Input;
using System;

namespace TeamPicks
{
    public partial class MainWindow : Window
    {
        readonly string fileName = "somebodysaveme.txt";
        public MainViewModel MainViewModel { get; set; }
        public ICommand MinimizeCommand => new RelayCommand(o => WindowState = WindowState.Minimized);
        public ICommand MaximizeCommand => new RelayCommand(o => WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized);
        public ICommand CloseCommand => new RelayCommand(o => Close());
        public ICommand ToggleViewCommand => new RelayCommand(o => MainViewModel.UseTileView = !MainViewModel.UseTileView);
        public ICommand ToggleChampionPoolCommand => new RelayCommand(o => MainViewModel.ShowChampionPool = !MainViewModel.ShowChampionPool);
        public ICommand ToggleExpandedState => new RelayCommand(o => OnToggleExpandedState());
        public ICommand ResetStates => new RelayCommand(o => MainViewModel?.ResetChamptionStates());
        public ICommand ClearPlayers => new RelayCommand(o => MainViewModel?.ClearPlayerPicks());

        public MainWindow()
        {
            InitializeComponent();
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
            DataContext = this;
            MainViewModel = new MainViewModel(new WebCrawler());
            MainViewModel.Initialize();
            LoadFile();
        }

        private void OnToggleExpandedState()
        {
            var players = MainViewModel.Players.Cast<Player>().ToList();

            bool newExpandedState = !players.Any(p => p.IsExpanded);
            players.ForEach(player => player.IsExpanded = newExpandedState);
        }

        private void LoadFile()
        {
            List<PlayerData> playerDataFromFile;
            try
            {
                var settings = System.IO.File.ReadAllText(fileName);
                playerDataFromFile = JsonConvert.DeserializeObject<List<PlayerData>>(settings);
            }

            catch (Exception)
            {
                 var defaultPlayers = new []
                {
                    new Player{Name = "Top"},
                    new Player{Name = "Mid"},
                    new Player{Name = "Jungle"},
                    new Player{Name = "Adc"},
                    new Player{Name = "Support"},
                };

                MainViewModel.Players.Add(defaultPlayers);

                return;
            }

            var existingChampions = MainViewModel.Champions as List<Champion>;

            foreach (var playerData in playerDataFromFile)
            {
                var player = new Player { Name = playerData.Name };

                foreach (var championName in playerData.MainPicks)
                {
                    var existingChampion = existingChampions.TrySingleOrDefault(c => c.Name == championName);
                    if (existingChampion != null)
                    {
                        player.ChampionsMain.Add(existingChampion);
                    }
                }

                foreach (var championName in playerData.AlternativePicks)
                {
                    var existingChampion = existingChampions.TrySingleOrDefault(c => c.Name == championName);
                    if (existingChampion != null)
                    {
                        player.ChampionsGood.Add(existingChampion);
                    }
                }

                foreach (var championName in playerData.OtherPicks)
                {
                    var existingChampion = existingChampions.TrySingleOrDefault(c => c.Name == championName);
                    if (existingChampion != null)
                    {
                        player.ChampionsReserve.Add(existingChampion);
                    }
                }

                MainViewModel.Players.Add(player);
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            var settings = SerializationService.Serialize(MainViewModel.Players as List<Player>);
            System.IO.File.WriteAllText(fileName, settings);
            base.OnClosing(e);
        }
    }

    public static class Extensions
    {
        public static T TrySingleOrDefault<T>(this IEnumerable<T> source, Func<T, bool> predicate) where T : class
        {
            try
            {
                return source.SingleOrDefault(predicate);
            }
            catch
            {
                return null;
            }
        }
    }
}
