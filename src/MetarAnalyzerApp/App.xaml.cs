using MetarAnalyzerApp.ViewModel;
using System.Windows;

namespace MetarAnalyzerApp
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow();
            var mainViewModel = new MainViewModel();
            MainWindow.DataContext = mainViewModel;
            MainWindow.Show();
            ViewModelManager.MainViewModel = mainViewModel;
            base.OnStartup(e);
        }
    }
}
