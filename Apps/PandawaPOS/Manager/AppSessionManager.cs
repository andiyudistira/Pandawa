namespace PandawaPOS.Manager
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Threading;
    using Siska.Core;
    using Siska.Data.Dto.Pos;
    using Siska.Service;
    using Siska.Wpf.Manager;

    public class AppSessionManager : IAppSessionManager, IDisposable
    {
        private BackgroundWorker loginWorker;
        private BackgroundWorker initialLoginWorker;

        private UserDto authUser;
        private bool isAuthenticated;
        private bool isLoginInitialised;
        private int wrongAuthCount;
        
        public Guid SessionId { get; set; }

        public SEnvironment.Constants.LogonStatus LogonStatus { get; set; }

        public UserDto AuthUser 
        {
            get { return authUser; }
            set
            {
                authUser = value;
                this.OnPropertyChanged("AuthUser");
            }
        }

        public bool IsAuthenticated 
        {
            get { return isAuthenticated; }
            set
            {
                isAuthenticated = value;
                this.OnPropertyChanged("IsAuthenticated");

                if (value && UserAuthenticated != null)
                {
                    UserAuthenticated(AuthUser, null);
                }
            }
        }

        public bool IsLoginInitialised
        {
            get { return isLoginInitialised; }
            set
            {
                isLoginInitialised = value;
                this.OnPropertyChanged("IsLoginInitialised");

                if (value && LogonInitialCompleted != null)
                {
                    LogonInitialCompleted(AuthUser, null);
                }
            }
        }

        public int WrongAuthCount 
        {
            get { return wrongAuthCount; }
            set
            {
                wrongAuthCount = value;
                this.OnPropertyChanged("WrongAuthCount");
            }
        }

        private ISessionService SessionService;
        public event EventHandler UserAuthenticated;
        public event EventHandler LogonInitialCompleted;

        public AppSessionManager(ISessionService sessionService)
        {
            loginWorker = new BackgroundWorker();
            initialLoginWorker = new BackgroundWorker();

            loginWorker.DoWork += loginWorker_DoWork;
            loginWorker.RunWorkerCompleted += loginWorker_RunWorkerCompleted;

            initialLoginWorker.DoWork += initialLoginWorker_DoWork;
            initialLoginWorker.RunWorkerCompleted += initialLoginWorker_RunWorkerCompleted;

            AuthUser = new UserDto();
            wrongAuthCount = 0;

            SessionService = sessionService;
        }

        void initialLoginWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IsLoginInitialised = true;
        }

        void initialLoginWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            ServiceResponse response = SessionService.LogonStatus();

            if (response != null && response.IsSuccess)
            {
                SEnvironment.Constants.LogonStatus result = (SEnvironment.Constants.LogonStatus)response.Data;

                if (result == SEnvironment.Constants.LogonStatus.LoggedOn)
                {
                    response = SessionService.LastLoggedOnUser();

                    if (response.IsSuccess)
                    {                        
                        UserSessionDto userSession = response.Data as UserSessionDto;
                        AuthUser = userSession.User;
                        SessionId = userSession.SessionId;

                        if (AuthUser != null)
                        {
                            LogonStatus = SEnvironment.Constants.LogonStatus.Locked;
                            e.Result = true;
                        }
                        else
                        {
                            e.Result = false;
                        }
                    }
                    else
                    {
                        e.Result = false;
                    }
                }
            }
            else
            {
                e.Result = false;
            }
        }

        private void loginWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (Convert.ToBoolean(e.Result))
            {
                IsAuthenticated = true;
            }
            else
            {
                wrongAuthCount++;
            }
        }

        private void loginWorker_DoWork(object sender, DoWorkEventArgs e)
        {            
            // do login in here
            ServiceRequest req = new ServiceRequest();
            req.NoData = true;

            Hashtable serviceParams = new Hashtable();

            serviceParams.Add(ServiceConstants.USERNAME, authUser.UserName);
            serviceParams.Add(ServiceConstants.PASSWORD, authUser.Password);
            serviceParams.Add(ServiceConstants.RECORDSTATUS, true);

            req.Parameters = serviceParams;

            ServiceResponse serviceResult = SessionService.StartSession(req);

            if (serviceResult.IsSuccess)
            {
                AuthUser = serviceResult.Data as UserDto;

                if (AuthUser != null)
                {
                    e.Result = true;
                }
                else
                {
                    e.Result = false;
                }
            }
            else
            {
                e.Result = false;
            }            
        }

        public void Login(string userName, string password)
        {
            authUser.UserName = userName;
            authUser.Password = password;

            loginWorker.RunWorkerAsync();
        }

        public void CheckLoginStatus()
        {
            initialLoginWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        public virtual void OnPropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposeBool)
        {
            if (disposeBool)
            {
                lock (this)
                {
                    if (loginWorker != null)
                    {
                        loginWorker.Dispose();
                    }

                    if (initialLoginWorker != null)
                    {
                        initialLoginWorker.Dispose();
                    }
                }
            }
        }
    }
}
