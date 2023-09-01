using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.Operators
{
    public class LogicNotEqualsBoolAndBool : IOperator
    {
        public string OperatorName => "!=";

        public bool IsOneParameterOperator => false;

        public OperatorGrade Grade => OperatorGrade.Third;

        public ReferenceName Type1 => "bool";

        public ReferenceName Type2 => "bool";

        public ReferenceName Result => "bool";

        public ObjectHolder Process(IFunction obj1, IFunction obj2, VariableHolder vHolder)
        {
            return new ObjectHolder((bool)obj1.GetResult(vHolder).Object != (bool)obj2.GetResult(vHolder).Object, "bool");
        }
    }
}
