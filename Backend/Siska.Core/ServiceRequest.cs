using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siska.Core
{
    public class ServiceRequest
    {
        public bool NoParameters { get; set; }
        public bool NoData { get; set; }
        public Hashtable Parameters { get; set; }
        public Hashtable Data { get; set; }

        public ServiceRequest()
        {
            NoParameters = false;
            NoData = false;
        }
    }
}
