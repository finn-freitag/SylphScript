using SylphScript.Functions;
using SylphScript.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.AdditionalParsers
{
    public class NullParser : IAdditionalParser
    {
        public (IFunction Function, bool Success) Parse(ref int index, string code, VariableHolder vHolder)
        {
            if (ParserHelper.CheckPos(index, code, "null"))
            {
                index += 4;
                return (new _constFunction(ObjectHolder.Null), true);
            }
            return (null, false);
        }
    }
}
