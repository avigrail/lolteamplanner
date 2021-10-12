using System;
using System.Windows.Input;

namespace TeamPicks
{
    public class Champion : ViewModelBase
    {
        public ICommand ToggleIsEnabled => new RelayCommand(o => SetOpacity(1.2 - Opacity));
        public ICommand OpenStatsInBrowser => new RelayCommand(OnOpenStats);

        private void OnOpenStats(object obj)
        {
            System.Diagnostics.Process.Start($"https://euw.op.gg/champion/{Name}");
        }

        public double Opacity { get; set; } = 1.0;
        public string ImageUrl { get; set; }
        public string Name { get; set; }

        internal void SetOpacity(double value)
        {
            Opacity = value;
            OnPropertyChanged(nameof(Opacity));
        }
    }
}