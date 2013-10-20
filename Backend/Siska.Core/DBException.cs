namespace Siska.Core
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class DBException : Exception
    {
        public DBException()
        {
        }

        public DBException(string message)
            : base(message)
        {
        }

        public DBException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }

        public DBException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public DBException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException)
        {
        }

        protected DBException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
