using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.Conversions
{
    public class DoubleToInteger : IConversion
    {
        public bool Implicit => false;

        public ReferenceName Origin => "double";

        public ReferenceName Target => "int";

        public ObjectHolder Convert(ObjectHolder value)
        {
            return new ObjectHolder((int)(double)value.Object, "int");
        }
    }
}
