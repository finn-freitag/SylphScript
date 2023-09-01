using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.Operators
{
    public class LogicNot : IOperator
    {
        public string OperatorName => "!";

        public bool IsOneParameterOperator => true;

        public OperatorGrade Grade => OperatorGrade.First;

        public ReferenceName Type1 => "bool";

        public ReferenceName Type2 => "";

        public ReferenceName Result => "bool";

        public ObjectHolder Process(IFunction obj1, IFunction obj2, VariableHolder vHolder)
        {
            return new ObjectHolder(!(bool)obj1.GetResult(vHolder).Object, "bool");
        }
    }
}
