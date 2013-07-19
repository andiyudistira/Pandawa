
namespace Siska.Wpf.Manager
{
    using System;
    using System.Collections.Generic;

    public class MenuNames
    {
        public IDictionary<PosMenus, Uri> MenuList { get; set; }

        public MenuNames()
        {
            MenuList = new Dictionary<PosMenus, Uri>();

            MenuList.Add(PosMenus.Login, new Uri("/View/Pages/LoginView.xaml", UriKind.Relative));
            MenuList.Add(PosMenus.RetailTransaction, new Uri("/View/Pages/RetailTransaction.xaml", UriKind.Relative));
            MenuList.Add(PosMenus.PurchaseOrderTransaction, new Uri("/View/Pages/Introduction.xaml", UriKind.Relative));
            MenuList.Add(PosMenus.InventoryTransaction, new Uri("/View/Pages/Introduction.xaml", UriKind.Relative));
            MenuList.Add(PosMenus.TransactionDashboard, new Uri("/View/Pages/DashboardPos.xaml", UriKind.Relative));
            MenuList.Add(PosMenus.InventoryDashboard, new Uri("/View/Pages/DashboardInventory.xaml", UriKind.Relative));
            MenuList.Add(PosMenus.PosReport, new Uri("/View/Pages/DashboardPos.xaml", UriKind.Relative));
            MenuList.Add(PosMenus.InventoryReport, new Uri("/View/Pages/DashboardInventory.xaml", UriKind.Relative));
        }
    }

    public enum PosMenus
    {
        Login = 1,
        RetailTransaction = 2,
        PurchaseOrderTransaction = 3,
        InventoryTransaction = 4,
        TransactionDashboard = 5,
        InventoryDashboard = 6,
        PosReport = 7,
        InventoryReport = 8,
    }
}
