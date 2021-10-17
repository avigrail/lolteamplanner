using System.ComponentModel;
using System.Windows;
using System.Linq;
using System.Windows.Input;
using System;
using TeamTierList.Core;

namespace TeamTierList.UI
{
    public partial class MainWindow : Window
    {
        private readonly string fileName = "teamtierlist.json";
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
            MainViewModel = new MainViewModel(new UggCrawler());
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
            try
            {
                var dataFromFile = System.IO.File.ReadAllText(fileName);
                SerializationService.Deserialize(dataFromFile, MainViewModel);                
            }

            catch (Exception)
            {
                var defaultPlayers = new[]
                {
                    new Player{Name = "Top"},
                    new Player{Name = "Mid"},
                    new Player{Name = "Jungle"},
                    new Player{Name = "Adc"},
                    new Player{Name = "Support"},
                };

                MainViewModel.Players.AddRange(defaultPlayers);

                return;
            }            
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            var userData = SerializationService.Serialize(MainViewModel);
            System.IO.File.WriteAllText(fileName, userData);

            base.OnClosing(e);
        }
    }    
}
