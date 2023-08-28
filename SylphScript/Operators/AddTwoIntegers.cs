using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.Operators
{
    public class AddTwoIntegers : IOperator
    {
        public string OperatorName => "+";

        public ReferenceName Type1 => "int";

        public ReferenceName Type2 => "int";

        public ReferenceName Result => "int";

        public ObjectHolder Process(ObjectHolder obj1, ObjectHolder obj2)
        {
            return new ObjectHolder((int)obj1.Object + (int)obj2.Object, "int");
        }
    }
}
