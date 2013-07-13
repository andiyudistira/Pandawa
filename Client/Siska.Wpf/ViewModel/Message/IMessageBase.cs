namespace Siska.Wpf.ViewModel.Message
{
    using System;

    interface IMessageBase
    {
        object Data { get; set; }
        Exception Exception { get; set; }
        ProcStatus Status { get; set; }
    }
}
