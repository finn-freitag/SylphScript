using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.Conversions
{
    public class IntegerToByte : IConversion
    {
        public bool Implicit => false;

        public ReferenceName Origin => "int";

        public ReferenceName Target => "byte";

        public ObjectHolder Convert(ObjectHolder value)
        {
            return new ObjectHolder((byte)(int)value.Object, "byte");
        }
    }
}
