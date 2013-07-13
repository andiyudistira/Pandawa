namespace PandawaPOS.ViewModel
{
    using GalaSoft.MvvmLight.Messaging;
    using GalaSoft.MvvmLight.Threading;
    using Siska.Wpf.Manager;
    using Siska.Wpf.ViewModel;
    using Siska.Wpf.ViewModel.Message;

    public class MainViewModel : SiskaViewModel
    {
        private IMenuManager menuManager;

        public IMenuManager MenuManager
        {
            get { return this.menuManager; }
            set
            {
                this.menuManager = value;
                this.OnPropertyChanged("MenuManager");
            }
        }

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
    }
}