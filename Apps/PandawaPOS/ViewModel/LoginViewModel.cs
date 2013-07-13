namespace PandawaPOS.ViewModel
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Messaging;
    using NHibernate.Validator.Constraints;
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

        private string password;

        private bool canSignIn;
        #endregion

        #region Property
        
        public RelayCommand<string> LogInClick { get; set; }

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

        [NotNullNotEmpty]
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

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");                
            }
        }
        #endregion

        #region Constructor
        public LoginViewModel(IAppSessionManager appSession) 
            : base(appSession)
        {
            LogInClick = new RelayCommand<string>(DoLogin);

            ResxLocalizationProvider.Instance.UpdateCultureList("PandawaPOS", "PandawaUI");
            LocalizeDictionary.Instance.PropertyChanged += Instance_PropertyChanged;

            ShowProgressBar = Visibility.Collapsed;
            canSignIn = false;

            AppSessionManager.UserAuthenticated += AppSessionManager_UserAuthenticated;
        }
        #endregion

        #region Private Method
        private void AppSessionManager_UserAuthenticated(object sender, EventArgs e)
        {
            ShowProgressBar = Visibility.Collapsed;
            Messenger.Default.Send(new LoginMessage());
        }

        private void DoLogin(string a)
        {
            ShowProgressBar = Visibility.Visible;

            AppSessionManager.Login(UserName, Password);
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
            return GetAllInvalidRules().Count == 0;
        }
        #endregion
    }
}
