using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.Conversions
{
    public class ByteToInteger : IConversion
    {
        public bool Implicit => true;

        public ReferenceName Origin => "byte";

        public ReferenceName Target => "int";

        public ObjectHolder Convert(ObjectHolder value)
        {
            return new ObjectHolder((int)(byte)value.Object, "int");
        }
    }
}
