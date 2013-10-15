namespace PandawaPOS.Manager
{
    using System;
    using System.Windows.Input;
    using Siska.Wpf.Manager;
    using FirstFloor.ModernUI.Presentation;
    using WPFLocalizeExtension.Extensions;
    using System.ComponentModel;
    using System.Collections.Generic;

    public class MenuManager : IMenuManager
    {
        private MenuNames posLinks;
        private Dictionary<PosMenus, List<object>> menuHotKey;

        public Dictionary<PosMenus, List<object>> MenuHotKey
        {
            get { return menuHotKey; }
            set 
            { 
                this.menuHotKey = value;
                this.OnPropertyChanged("MenuHotKey");
            }
        }

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MenuManager()
        {
            posLinks = new MenuNames();
            PosMenu = new LinkGroupCollection();
            CreateMenuHotKey();
        }

        private void CreateMenuHotKey()
        {
            menuHotKey = new Dictionary<PosMenus, List<object>>();

            // Transaction dashboard hotkey
            List<object> transDashboardKey = new List<object>();
            transDashboardKey.Add(Key.F1);
            menuHotKey.Add(PosMenus.TransactionDashboard, transDashboardKey);

            // Inventory dashboard hotkey
            List<object> inventDashboardKey = new List<object>();
            inventDashboardKey.Add(Key.F2);
            menuHotKey.Add(PosMenus.InventoryDashboard, inventDashboardKey);

            // Retail transaction hotkey
            List<object> retTransactionKey = new List<object>();
            retTransactionKey.Add(Key.F3);
            menuHotKey.Add(PosMenus.RetailTransaction, retTransactionKey);

            // Inventory transaction hotkey
            List<object> inventTransactionKey = new List<object>();
            inventTransactionKey.Add(Key.F4);
            menuHotKey.Add(PosMenus.InventoryTransaction, inventTransactionKey);

            // Purchase order transaction hotkey
            List<object> poTransactionKey = new List<object>();
            poTransactionKey.Add(Key.F5);
            menuHotKey.Add(PosMenus.PurchaseOrderTransaction, poTransactionKey);

            // Transaction report hotkey
            List<object> transReportKey = new List<object>();
            transReportKey.Add(Key.F6);
            menuHotKey.Add(PosMenus.PosReport, transReportKey);

            // Inventory report hotkey
            List<object> inventReportKey = new List<object>();
            inventReportKey.Add(Key.F7);
            menuHotKey.Add(PosMenus.InventoryReport, inventReportKey); 
        }

        public void CreateMenu(bool isAuthenticated)
        {
            this.PosMenu.Clear();

            if (isAuthenticated)
            {               
                CreateAuthMenu();
                this.SelectedLink = posLinks.MenuList[PosMenus.RetailTransaction];
            }
            else
            {
                CreateLoginMenu();
                this.SelectedLink = posLinks.MenuList[PosMenus.Login];
            }
        }

        private void CreateLoginMenu()
        {
            var linkTestGroup1 = new LinkGroup { DisplayName = "Pandawa POS" };
            var link1 = new Link { DisplayName = "Login", Source = posLinks.MenuList[PosMenus.Login] };
            linkTestGroup1.Links.Add(link1);

            this.PosMenu.Add(linkTestGroup1);
        }

        private void CreateAuthMenu()
        {
            var linkDashboardGroup = new LinkGroup { DisplayName = "Dashboard" };
            var linkDashboardPos = new Link { DisplayName = "Transaction", Source = posLinks.MenuList[PosMenus.TransactionDashboard] };
            var linkDashboardInventory = new Link { DisplayName = "Inventory", Source = posLinks.MenuList[PosMenus.InventoryDashboard] };
            linkDashboardGroup.Links.Add(linkDashboardPos);
            linkDashboardGroup.Links.Add(linkDashboardInventory);            

            var linkTestGroup1 = new LinkGroup { DisplayName = GetUIString("PandawaPOS:PandawaUI:Menu_Transaction") };
            var link1 = new Link { DisplayName = GetUIString("PandawaPOS:PandawaUI:Menu_PoS"), Source = posLinks.MenuList[PosMenus.RetailTransaction] };
            var link2 = new Link { DisplayName = GetUIString("PandawaPOS:PandawaUI:Menu_PurchaseOrder"), Source = posLinks.MenuList[PosMenus.PurchaseOrderTransaction] };
            var link3 = new Link { DisplayName = "Inventory", Source = posLinks.MenuList[PosMenus.InventoryTransaction] };
            linkTestGroup1.Links.Add(link1);
            linkTestGroup1.Links.Add(link2);
            linkTestGroup1.Links.Add(link3);

            var linkReportGroup = new LinkGroup { DisplayName = "Laporan" };
            var linkReportPos = new Link { DisplayName = "Pos", Source = posLinks.MenuList[PosMenus.PosReport] };
            var linkReportInventory = new Link { DisplayName = "Inventory", Source = posLinks.MenuList[PosMenus.InventoryReport] };
            linkReportGroup.Links.Add(linkReportPos);
            linkReportGroup.Links.Add(linkReportInventory);    

            this.PosMenu.Insert(0, linkDashboardGroup);
            this.PosMenu.Insert(1, linkTestGroup1);
            this.PosMenu.Insert(2, linkReportGroup);
        }

        public void ReloadMenu(bool isAuthenticated)
        {
            this.PosMenu.Clear();

            if (isAuthenticated)
            {               
                CreateAuthMenu();
                this.SelectedLink = posLinks.MenuList[PosMenus.RetailTransaction];
            }
            else
            {
                CreateLoginMenu();
                this.SelectedLink = posLinks.MenuList[PosMenus.Login];
            }
        }

        public void NavigateToMenu(PosMenus menu)
        {
            this.SelectedLink = posLinks.MenuList[menu];
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
