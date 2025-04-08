using Avalonia.Controls;
using GeometryApp.ViewModels;

namespace GeometryApp.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new GeometryViewModel();
        }
    }
}