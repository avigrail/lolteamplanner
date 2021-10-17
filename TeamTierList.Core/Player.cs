using System.Collections.ObjectModel;
using System.Windows.Input;

namespace TeamTierList.Core
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
            TierS?.Clear();
            TierA?.Clear();
            TierB?.Clear();
        });

        public ObservableCollection<Champion> TierS { get; set; } = new ObservableCollection<Champion>();
        public ObservableCollection<Champion> TierA { get; set; } = new ObservableCollection<Champion>();
        public ObservableCollection<Champion> TierB { get; set; } = new ObservableCollection<Champion>();
        public string Name { get; set; }
    }
}