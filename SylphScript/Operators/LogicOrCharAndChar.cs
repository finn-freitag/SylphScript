using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.Operators
{
    public class LogicOrCharAndChar : IOperator
    {
        public string OperatorName => "|";

        public bool IsOneParameterOperator => false;

        public OperatorGrade Grade => OperatorGrade.Third;

        public ReferenceName Type1 => "char";

        public ReferenceName Type2 => "char";

        public ReferenceName Result => "char";

        public ObjectHolder Process(IFunction obj1, IFunction obj2, VariableHolder vHolder)
        {
            return new ObjectHolder((char)((char)obj1.GetResult(vHolder).Object | (char)obj2.GetResult(vHolder).Object), "char");
        }
    }
}
