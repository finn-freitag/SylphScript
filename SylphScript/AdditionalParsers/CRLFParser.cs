using SylphScript.Functions;
using SylphScript.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.AdditionalParsers
{
    public class CRLFParser : IAdditionalParser
    {
        public bool isTypeParser => true;

        public (IFunction Function, bool Success) Parse(ref int index, string code, VariableHolder vHolder)
        {
            if (ParserHelper.CheckPos(index, code, "CRLF"))
            {
                index += 4;
                if (char.IsLetter(code[index])) return (null, false);
                return (new _constFunction(new ObjectHolder(Environment.NewLine, "string")), true);
            }
            else return (null, false);
        }
    }
}
