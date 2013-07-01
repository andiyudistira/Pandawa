using System;
using System.Windows;
using GalaSoft.MvvmLight.Threading;

namespace PandawaPOS
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            StartupUri = new Uri("/PandawaPOS;component/View/LoginView.xaml",
                    UriKind.Relative);
        }
    }
}
