using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.Operators
{
    public class DivideTwoDoubles : IOperator
    {
        public string OperatorName => "/";

        public bool IsOneParameterOperator => false;

        public OperatorGrade Grade => OperatorGrade.First;

        public ReferenceName Type1 => "double";

        public ReferenceName Type2 => "double";

        public ReferenceName Result => "double";

        public ObjectHolder Process(IFunction obj1, IFunction obj2, VariableHolder vHolder)
        {
            return new ObjectHolder((double)obj1.GetResult(vHolder).Object / (double)obj2.GetResult(vHolder).Object, "double");
        }
    }
}
