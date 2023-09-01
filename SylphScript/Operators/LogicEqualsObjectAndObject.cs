using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.Operators
{
    public class LogicEqualsObjectAndObject : IOperator
    {
        public string OperatorName => "==";

        public bool IsOneParameterOperator => false;

        public OperatorGrade Grade => OperatorGrade.Third;

        public ReferenceName Type1 => "object";

        public ReferenceName Type2 => "object";

        public ReferenceName Result => "bool";

        public ObjectHolder Process(IFunction obj1, IFunction obj2, VariableHolder vHolder)
        {
            return new ObjectHolder(obj1.GetResult(vHolder).Object.Equals(obj2.GetResult(vHolder).Object), "bool");
        }
    }
}
