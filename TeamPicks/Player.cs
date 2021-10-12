using System.Collections.ObjectModel;
using System.Windows.Input;

namespace TeamPicks
{
    public class Player : ViewModelBase
    {
        public bool IsExpanded
        {
            get => isExpanded;
            set
            {
                isExpanded = value;
                OnPropertyChanged();
            }
        }

        private bool isExpanded = true;

        public ICommand ClearChampions => new RelayCommand(_ =>
        {
            ChampionsMain?.Clear();
            ChampionsGood?.Clear();
            ChampionsReserve?.Clear();
        });

        public ObservableCollection<Champion> ChampionsMain { get; set; } = new ObservableCollection<Champion>();
        public ObservableCollection<Champion> ChampionsGood { get; set; } = new ObservableCollection<Champion>();
        public ObservableCollection<Champion> ChampionsReserve { get; set; } = new ObservableCollection<Champion>();
        public string Name { get; set; }
    }
}