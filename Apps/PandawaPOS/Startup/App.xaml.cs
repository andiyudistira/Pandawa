using System;
using System.Windows;
using FirstFloor.ModernUI.Presentation;
using GalaSoft.MvvmLight.Threading;
using WPFLocalizeExtension.Engine;

namespace PandawaPOS
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            StartupUri = new Uri("/PandawaPOS;component/View/MainWindow.xaml",
                    UriKind.Relative);

            DispatcherHelper.Initialize();

            AppearanceManager.Current.ThemeSource = new Uri("/PandawaPOS;component/Assets/ModernUI.Snowflakes.xaml", UriKind.Relative);

            LocalizeDictionary.Instance.Culture = new System.Globalization.CultureInfo("id-ID");            
        }
    }
}
