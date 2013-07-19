using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siska.Wpf
{
    public class Dialogs
    {
        public enum PosDialogResult
        {
            Yes = 1,
            No = 2,
            Ok = 3,
            Cancel = 4,
            NoDialog = 5,
        }

        public enum PosDialogs
        {
            ItemDialogContent = 1,
            PaidDialogContent = 2,
        }
    }
}
