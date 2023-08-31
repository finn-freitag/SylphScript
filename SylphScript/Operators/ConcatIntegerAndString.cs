using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.Operators
{
    public class ConcatIntegerAndString : IOperator
    {
        public string OperatorName => "+";

        public bool IsOneParameterOperator => false;

        public OperatorGrade Grade => OperatorGrade.Third;

        public ReferenceName Type1 => "int";

        public ReferenceName Type2 => "string";

        public ReferenceName Result => "string";

        public ObjectHolder Process(IFunction obj1, IFunction obj2, VariableHolder vHolder)
        {
            return new ObjectHolder((int)obj1.GetResult(vHolder).Object + (string)obj2.GetResult(vHolder).Object, "string");
        }
    }
}
