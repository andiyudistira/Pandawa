using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Siska.Core
{
    [Serializable]
    public class DaoException : Exception
    {
        public DaoException()
        {
        }

        public DaoException(string message)
            : base(message)
        {
        }

        public DaoException(string format, params object[] args)
            : base(string.Format(format, args)) 
        { 
        }

        public DaoException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public DaoException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException)
        {
        }

        protected DaoException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
