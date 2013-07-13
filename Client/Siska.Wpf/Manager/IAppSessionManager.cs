namespace Siska.Wpf.Manager
{
    using System;
    using System.ComponentModel;

    public interface IAppSessionManager : INotifyPropertyChanged
    {
        Siska.Data.Dto.Pos.UserDto AuthUser { get; set; }
        bool IsAuthenticated { get; set; }
        int WrongAuthCount { get; set; }

        event EventHandler UserAuthenticated;

        void Login(string userName, string password);
    }
}
