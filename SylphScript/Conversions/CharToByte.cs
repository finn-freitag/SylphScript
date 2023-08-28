using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.Conversions
{
    public class CharToByte : IConversion
    {
        public bool Implicit => true;

        public ReferenceName Origin => "char";

        public ReferenceName Target => "byte";

        public ObjectHolder Convert(ObjectHolder value)
        {
            return new ObjectHolder((char)(byte)value.Object, "char");
        }
    }
}
