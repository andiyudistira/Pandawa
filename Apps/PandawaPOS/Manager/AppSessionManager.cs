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

    public class AppSessionManager : IAppSessionManager
    {
        private BackgroundWorker loginWorker = new BackgroundWorker();
        private UserDto authUser;
        private bool isAuthenticated;
        private int wrongAuthCount;

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

        public AppSessionManager(ISessionService sessionService)
        {
            IsAuthenticated = false;

            loginWorker.DoWork += loginWorker_DoWork;
            loginWorker.RunWorkerCompleted += loginWorker_RunWorkerCompleted;

            AuthUser = new UserDto();
            wrongAuthCount = 0;

            SessionService = sessionService;
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
    }
}
