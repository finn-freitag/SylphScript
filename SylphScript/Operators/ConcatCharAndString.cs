using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.Operators
{
    public class ConcatCharAndString : IOperator
    {
        public string OperatorName => "+";

        public ReferenceName Type1 => "char";

        public ReferenceName Type2 => "string";

        public ReferenceName Result => "string";

        public ObjectHolder Process(ObjectHolder obj1, ObjectHolder obj2)
        {
            return new ObjectHolder((char)obj1.Object + (string)obj2.Object, "string");
        }
    }
}
