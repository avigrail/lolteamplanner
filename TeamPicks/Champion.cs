using System.Windows.Input;

namespace TeamPicks
{
    public class Champion : ViewModelBase
    {
        public ICommand ToggleIsEnabled => new RelayCommand(o => { Opacity = 1.2 - Opacity; OnPropertyChanged(nameof(Opacity)); });
        public double Opacity { get; set; } = 1.0;
        public string ImageUrl { get; set; }
        public string Name { get; set; }
    }
}