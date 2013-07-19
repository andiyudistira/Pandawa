namespace Siska.Wpf.Manager
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public interface IMenuManager : INotifyPropertyChanged
    {
        void CreateMenu(bool isAuthenticated);
        FirstFloor.ModernUI.Presentation.LinkGroupCollection PosMenu { get; set; }
        Dictionary<PosMenus, List<object>> MenuHotKey { get; set; }
        void NavigateToMenu(PosMenus menu);
        string SelectedLinkUri { get; }
        Uri SelectedLink { get; set; }
        void ReloadMenu(bool isAuthenticated);
    }
}
