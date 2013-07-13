namespace PandawaPOS.Manager
{
    using System;
    using Siska.Wpf.Manager;
    using FirstFloor.ModernUI.Presentation;
    using WPFLocalizeExtension.Extensions;
    using System.ComponentModel;

    public class MenuManager : IMenuManager
    {
        private MenuNames posLinks;

        private LinkGroupCollection posMenu;
        public LinkGroupCollection PosMenu
        {
            get { return this.posMenu; }
            set
            {
                this.posMenu = value;
                this.OnPropertyChanged("PosMenu");
            }
        }

        public string SelectedLinkUri
        {
            get { return this.SelectedLink.OriginalString; }
        }

        private Uri selectedLink;
        public Uri SelectedLink
        {
            get { return this.selectedLink; }
            set
            {
                this.selectedLink = value;
                this.OnPropertyChanged("SelectedLink");
            }
        }

        public MenuManager()
        {
            posLinks = new MenuNames();
            PosMenu = new LinkGroupCollection();
        }

        public void CreateMenu(bool isAuthenticated)
        {
            this.PosMenu.Clear();

            if (isAuthenticated)
            {               
                CreateAuthMenu();
                this.SelectedLink = posLinks.MenuList[MenuNames.RETAILTRANSACTION_MENU];
            }
            else
            {
                CreateLoginMenu();
                this.SelectedLink = posLinks.MenuList[MenuNames.LOGIN_MENU];
            }
        }

        private void CreateLoginMenu()
        {
            var linkTestGroup1 = new LinkGroup { DisplayName = "Pandawa POS", GroupName = MenuNames.LOGIN_GROUPMENU };
            var link1 = new Link { DisplayName = "Login", Source = posLinks.MenuList[MenuNames.LOGIN_MENU] };
            linkTestGroup1.Links.Add(link1);

            this.PosMenu.Add(linkTestGroup1);
        }

        private void CreateAuthMenu()
        {
            var linkTestGroup1 = new LinkGroup { DisplayName = GetUIString("PandawaPOS:PandawaUI:Menu_Transaction"), GroupName = MenuNames.TRANSACTION_GROUPMENU };
            var link1 = new Link { DisplayName = GetUIString("PandawaPOS:PandawaUI:Menu_PoS"), Source = posLinks.MenuList[MenuNames.RETAILTRANSACTION_MENU] };
            //var link2 = new Link { DisplayName = GetUIString("PandawaPOS:PandawaUI:Menu_PurchaseOrder"), Source = new Uri("/View/Pages/Introduction.xaml", UriKind.Relative) };
            linkTestGroup1.Links.Add(link1);
            //linkTestGroup1.Links.Add(link2);

            this.PosMenu.Add(linkTestGroup1);            
        }

        public void ReloadMenu(bool isAuthenticated)
        {
            this.PosMenu.Clear();

            if (isAuthenticated)
            {
                CreateAuthMenu();
                this.SelectedLink = posLinks.MenuList[MenuNames.RETAILTRANSACTION_MENU];
            }
            else
            {
                CreateLoginMenu();
                this.SelectedLink = posLinks.MenuList[MenuNames.LOGIN_MENU];
            }
        }

        private string GetUIString(string key)
        {
            string uiString;
            LocExtension locExtension = new LocExtension(key);
            locExtension.ResolveLocalizedValue(out uiString);
            return uiString;
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
