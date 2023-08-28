using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.Conversions
{
    public class IntegerToDouble : IConversion
    {
        public bool Implicit => true;

        public ReferenceName Origin => "int";

        public ReferenceName Target => "double";

        public ObjectHolder Convert(ObjectHolder value)
        {
            return new ObjectHolder((double)(int)value.Object, "double");
        }
    }
}
