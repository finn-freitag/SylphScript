using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.Operators
{
    public class LogicAndByteAndByte : IOperator
    {
        public string OperatorName => "&";

        public bool IsOneParameterOperator => false;

        public OperatorGrade Grade => OperatorGrade.Third;

        public ReferenceName Type1 => "byte";

        public ReferenceName Type2 => "byte";

        public ReferenceName Result => "byte";

        public ObjectHolder Process(IFunction obj1, IFunction obj2, VariableHolder vHolder)
        {
            return new ObjectHolder((byte)((byte)obj1.GetResult(vHolder).Object & (byte)obj2.GetResult(vHolder).Object), "byte");
        }
    }
}
