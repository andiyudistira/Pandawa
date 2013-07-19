namespace Siska.Wpf.Manager
{
    public interface IDialogManager
    {
        void CreateDialog(string dialogTitle, Dialogs.PosDialogs dialog);
        bool ShowDialog();      
    }
}
