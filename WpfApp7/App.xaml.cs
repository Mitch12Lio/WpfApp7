using System.Configuration;
using System.Data;
using System.Windows;

namespace WpfApp7
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MainWindow app = new MainWindow();
            ViewModels.MasterViewModel vmContext = new ViewModels.MasterViewModel();
            //ViewModels.CipherViewModel vmContext = new ViewModels.CipherViewModel();
            app.DataContext = vmContext;
            app.Show();

        }
    }

}
