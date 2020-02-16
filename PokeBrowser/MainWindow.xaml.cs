using PokeBrowser.ViewModels;

namespace PokeBrowser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MahApps.Metro.Controls.MetroWindow
    {
        public MainWindow()
        {
            DataContext = new MainWindowVm();
            InitializeComponent();
        }
    }
}
