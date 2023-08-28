using SylphScript.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.TypeParsers
{
    public class NullParser : ITypeParser
    {
        public (ObjectHolder Object, bool Success) Parse(ref int index, string code)
        {
            if (ParserHelper.CheckPos(index, code, "null"))
            {
                index += 4;
                return (ObjectHolder.Null, true);
            }
            return (null, false);
        }
    }
}
