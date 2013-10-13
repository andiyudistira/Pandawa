using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Siska.Core
{
   public class SEnvironment
   {
      //var fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DateLinks.xml")

       public class Constants
       {
           public enum LogonStatus
           {
               LoggedOff = 0,
               LoggedOn = 1,
               Locked = 2
           }
       }
   }
}
