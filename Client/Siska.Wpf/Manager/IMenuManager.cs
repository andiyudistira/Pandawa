namespace Siska.Wpf.Manager
{
    using System;
    using System.ComponentModel;

    public interface IMenuManager : INotifyPropertyChanged
    {
        void CreateMenu(bool isAuthenticated);
        FirstFloor.ModernUI.Presentation.LinkGroupCollection PosMenu { get; set; }
        string SelectedLinkUri { get; }
        Uri SelectedLink { get; set; }
        void ReloadMenu(bool isAuthenticated);
    }
}
