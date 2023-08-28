using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.Operators
{
    public class ConcatCharAndChar : IOperator
    {
        public string OperatorName => "+";

        public ReferenceName Type1 => "char";

        public ReferenceName Type2 => "char";

        public ReferenceName Result => "string";

        public ObjectHolder Process(ObjectHolder obj1, ObjectHolder obj2)
        {
            return new ObjectHolder("" + (char)obj1.Object + (char)obj2.Object, "string");
        }
    }
}
