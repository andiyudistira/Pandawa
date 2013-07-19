namespace PandawaPOS.Manager
{
    using System;    
    using Siska.Wpf;
    using Siska.Wpf.Manager;
    using FirstFloor.ModernUI.Windows.Controls;

    public class DialogManager : IDialogManager
    {
        private ModernDialog modernDialog;

        public void CreateDialog(string dialogTitle, Dialogs.PosDialogs dialog)
        {
            string mainAssembly = System.Configuration.ConfigurationManager.AppSettings["MainAssembly"].ToString();
            string assemblyName = System.Configuration.ConfigurationManager.AppSettings["AssemblyName"].ToString();
            string dialogContentNamespace = string.Format("{0}.{1}.{2}",assemblyName,"View","Dialogs");

            object dialogContent = Activator.CreateInstanceFrom(mainAssembly, string.Format("{0}.{1}", dialogContentNamespace, dialog)).Unwrap();

            modernDialog = new ModernDialog
            {
                Title = "Common dialog",
                Content = dialogContent
            };
        }

        public bool ShowDialog()
        {
            bool result = false;

            if (modernDialog != null)
            {
                modernDialog.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                modernDialog.ShowDialog();
                result = true;
            }

            return result;
        }
    }
}
