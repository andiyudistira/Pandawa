using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siska.Core
{
    public class ServiceResponse
    {
        public object Data { get; set; }
        public bool IsSuccess { get; set; }
        public Exception Exception { get; set; }

        public ServiceResponse()
        {
        }

        public ServiceResponse(bool success)
        {
            IsSuccess = success;
        }
    }
}
