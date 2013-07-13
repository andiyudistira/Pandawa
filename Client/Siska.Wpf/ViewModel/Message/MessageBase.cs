namespace Siska.Wpf.ViewModel.Message
{
    using System;

    public abstract class MessageBase : IMessageBase
    {
        public ProcStatus Status { get; set; }
        public object Data { get; set; }
        public Exception Exception { get; set; }
    }
}
