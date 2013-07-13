
namespace Siska.Wpf.Manager
{
    using System;
    using System.Collections.Generic;

    public class MenuNames
    {
        public static readonly string LOGIN_GROUPMENU = "Login";
        public static readonly string TRANSACTION_GROUPMENU = "Transaction";

        public static readonly string LOGIN_MENU = "Login";
        public static readonly string RETAILTRANSACTION_MENU = "RetailTransaction";
        public static readonly string PURCHASEORDER_MENU = "PurchaseOrderTransaction";

        public IDictionary<string, Uri> MenuList { get; set; }

        public MenuNames()
        {
            MenuList = new Dictionary<string, Uri>();

            MenuList.Add(LOGIN_MENU, new Uri("/View/Pages/LoginView.xaml", UriKind.Relative));
            MenuList.Add(RETAILTRANSACTION_MENU, new Uri("/View/Pages/RetailTransaction.xaml", UriKind.Relative));
        }
    }
}
