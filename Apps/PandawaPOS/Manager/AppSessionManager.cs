namespace PandawaPOS.Manager
{
    using System;
    using System.ComponentModel;
    using System.Threading;
    using Siska.Data.Dto.Pos;
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

        public event EventHandler UserAuthenticated;

        public AppSessionManager()
        {
            IsAuthenticated = false;

            loginWorker.DoWork += loginWorker_DoWork;
            loginWorker.RunWorkerCompleted += loginWorker_RunWorkerCompleted;

            AuthUser = new UserDto();
            wrongAuthCount = 0;
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
            Thread.Sleep(2000);
            // do login in here
            e.Result = true;
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
