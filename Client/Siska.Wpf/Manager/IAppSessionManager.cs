namespace Siska.Wpf.Manager
{
    using System;
    using System.ComponentModel;
    using Siska.Core;

    public interface IAppSessionManager : INotifyPropertyChanged
    {
        Guid SessionId { get; set; }
        Siska.Data.Dto.Pos.UserDto AuthUser { get; set; }
        bool IsAuthenticated { get; set; }
        int WrongAuthCount { get; set; }
        SEnvironment.Constants.LogonStatus LogonStatus { get; set; }

        event EventHandler UserAuthenticated;
        event EventHandler LogonInitialCompleted;

        void Login(string userName, string password);
        void CheckLoginStatus();
    }
}
