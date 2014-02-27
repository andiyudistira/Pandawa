namespace PandawaPOS.ViewModel
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Messaging;    
    using Siska.Wpf.Manager;
    using Siska.Wpf.ViewModel;
    using Siska.Wpf.ViewModel.Message;
    using WPFLocalizeExtension.Engine;
    using WPFLocalizeExtension.Providers;

    public class LoginViewModel : SiskaViewModel
    {
        #region Private member
        private Visibility showProgressBar;

        private string userName;

        private string passwordx;

        private bool canSignIn;
        #endregion

        #region Property
        
        public RelayCommand<string> LogInClick { get; set; }

        public RelayCommand<string> PageLoaded { get; set; }

        public bool CanSignIn
        {
            get { return canSignIn; }
            set
            {
                canSignIn = value;
                OnPropertyChanged("CanSignIn");
            }
        }

        public Visibility ShowProgressBar 
        {
            get { return showProgressBar; }
            set
            {
                showProgressBar = value;
                OnPropertyChanged("ShowProgressBar");
            }
        }

        //[NotNullNotEmpty]
        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                OnPropertyChanged("UserName");
                CanSignIn = CheckValidation();
            }
        }

        //[NotNullNotEmpty]
        public string Passwordx
        {
            get { return passwordx; }
            set
            {
                passwordx = value;
                OnPropertyChanged("Passwordx");
                CanSignIn = CheckValidation();
            }
        }
        #endregion

        #region Constructor
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LoginViewModel(IAppSessionManager appSession, IDialogManager dialogManager) 
            : base(appSession, dialogManager)
        {
            LogInClick = new RelayCommand<string>(DoLogin);
            PageLoaded = new RelayCommand<string>(LoginPageLoaded);

            ResxLocalizationProvider.Instance.UpdateCultureList("PandawaPOS", "PandawaUI");
            LocalizeDictionary.Instance.PropertyChanged += Instance_PropertyChanged;

            ShowProgressBar = Visibility.Collapsed;
            canSignIn = false;

            AppSessionManager.UserAuthenticated += AppSessionManager_UserAuthenticated;
            AppSessionManager.LogonInitialCompleted += AppSessionManager_LogonInitialCompleted;
        }
        #endregion

        #region Private Method
        private void AppSessionManager_UserAuthenticated(object sender, EventArgs e)
        {
            ShowProgressBar = Visibility.Collapsed;
            Messenger.Default.Send(new LoginMessage());
        }

        void AppSessionManager_LogonInitialCompleted(object sender, EventArgs e)
        {
            if (AppSessionManager.AuthUser != null)
            {
                UserName = AppSessionManager.AuthUser.UserName;
            }
            ShowProgressBar = Visibility.Collapsed;
        }

        private void LoginPageLoaded(string a)
        {
            ShowProgressBar = Visibility.Visible;
            AppSessionManager.CheckLoginStatus();
        }

        private void DoLogin(string a)
        {
            DialogManager.CreateDialog("Test", Siska.Wpf.Dialogs.PosDialogs.ItemDialogContent);
            DialogManager.ShowDialog();

            ShowProgressBar = Visibility.Visible;

            AppSessionManager.Login(UserName, Passwordx);
        }

        private void Instance_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.ToLower().Equals("culture"))
            {
                Messenger.Default.Send(new RefreshMenuMessage());
            }
        }
        #endregion

        #region Public Method
        public bool CheckValidation()
        {
            return true; // GetAllInvalidRules().Count == 0;
        }
        #endregion
    }
}
