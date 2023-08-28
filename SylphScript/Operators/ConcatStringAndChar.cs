using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.Operators
{
    public class ConcatStringAndChar : IOperator
    {
        public string OperatorName => "+";

        public ReferenceName Type1 => "string";

        public ReferenceName Type2 => "char";

        public ReferenceName Result => "string";

        public ObjectHolder Process(ObjectHolder obj1, ObjectHolder obj2)
        {
            return new ObjectHolder((string)obj1.Object + (string)obj2.Object, "string");
        }
    }
}
