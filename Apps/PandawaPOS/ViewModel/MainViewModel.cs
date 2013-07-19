namespace PandawaPOS.ViewModel
{
    using System.Linq;
    using System.Windows.Input; 
    using System.Collections.Generic;       
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Messaging;
    using GalaSoft.MvvmLight.Threading;
    using Siska.Wpf.Manager;
    using Siska.Wpf.ViewModel;
    using Siska.Wpf.ViewModel.Message;

    public class MainViewModel : SiskaViewModel
    {
        private IMenuManager menuManager;
        private List<Key> pressedKeys; 

        public IMenuManager MenuManager
        {
            get { return this.menuManager; }
            set
            {
                this.menuManager = value;
                this.OnPropertyChanged("MenuManager");
            }
        }

        public RelayCommand<KeyEventArgs> KeyDownCommand { get; set; }
        public RelayCommand<KeyEventArgs> KeyUpCommand { get; set; }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IAppSessionManager appSession, IMenuManager mManager)
            : base(appSession)
        {
            Messenger.Default.Register<RefreshMenuMessage>(this, HandleRefreshMenuProcess);
            Messenger.Default.Register<LoginMessage>(this, HandleLoginProcess);
            Messenger.Default.Register<NavigateMenuMessage>(this, HandleNavigateMenuProcess);
            menuManager = mManager;

            menuManager.CreateMenu(AppSessionManager.IsAuthenticated);

            pressedKeys = new List<Key>();
            KeyDownCommand = new RelayCommand<KeyEventArgs>(KeyDownCmd);
            KeyUpCommand = new RelayCommand<KeyEventArgs>(KeyUpCmd);
        }

        private void HandleLoginProcess(LoginMessage msg)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                menuManager.CreateMenu(AppSessionManager.IsAuthenticated);
            });            
        }

        private void HandleNavigateMenuProcess(NavigateMenuMessage msg)
        {
            //Link selLink = from a in MenuLinkGroups
        }

        private void HandleRefreshMenuProcess(RefreshMenuMessage msg)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                menuManager.ReloadMenu(AppSessionManager.IsAuthenticated);
            }); 
        }

        public void KeyUpCmd(KeyEventArgs e)
        {
            pressedKeys.Remove(e.Key);
        }

        public void KeyDownCmd(KeyEventArgs e)
        {
            Key cek = (from a in pressedKeys where a == e.Key select a).FirstOrDefault();

            if (cek == Key.None)
            {
                pressedKeys.Add(e.Key);

                foreach (var item in menuManager.MenuHotKey)
                {
                    List<Key> menuKey = item.Value.Cast<Key>().ToList();
                    
                    if (menuKey.Except(pressedKeys).ToList().FirstOrDefault() == Key.None)
                    {
                        MenuManager.NavigateToMenu(item.Key);
                    }
                }
            }
        }
    }
}