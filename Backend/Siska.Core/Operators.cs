using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Siska.Core
{
   public enum Aggregate
   {
      SUM = 1,
      MIN = 2,
      MAX = 3,
      AVG = 4,
      STDV = 5
   }

   public enum Operators
   {
      And = 1,
      Or = 2,
      NotEquals = 3,
      Equals = 4,
      Greater = 5,
      GreaterThan = 6,
      Smaller = 7,
      SmallerThan = 8,
      NotIn = 9,
      In = 10,
      Between = 11
   }

   public class CriteriaParam
   {
      public string FieldName { get; set; }
      public Operators Operator { get; set; }
      public object Value { get; set; }

      public CriteriaParam Left { get; set; }
      public CriteriaParam Right { get; set; }

      public object ValueLo { get; set; }
      public object ValueHigh { get; set; }
   }
}
