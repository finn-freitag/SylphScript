using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.Operators
{
    public class ConcatStringAndInteger : IOperator
    {
        public string OperatorName => "+";

        public bool IsOneParameterOperator => false;

        public OperatorGrade Grade => OperatorGrade.Third;

        public ReferenceName Type1 => "string";

        public ReferenceName Type2 => "int";

        public ReferenceName Result => "string";

        public ObjectHolder Process(IFunction obj1, IFunction obj2, VariableHolder vHolder)
        {
            return new ObjectHolder((string)obj1.GetResult(vHolder).Object + (int)obj2.GetResult(vHolder).Object, "string");
        }
    }
}
