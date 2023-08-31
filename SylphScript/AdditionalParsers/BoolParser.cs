using SylphScript.Functions;
using SylphScript.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.AdditionalParsers
{
    public class BoolParser : IAdditionalParser
    {
        public bool isTypeParser => true;

        public (IFunction Function, bool Success) Parse(ref int index, string code, VariableHolder vHolder)
        {
            int backupIndex = index;
            if (ParserHelper.CheckPos(index, code, "true"))
            {
                index += 4;
                if (index >= code.Length) return (new _constFunction(new ObjectHolder(true, "bool")), true);
                else if (!char.IsLetter(code[index])) return (new _constFunction(new ObjectHolder(true, "bool")), true);
            }
            index = backupIndex;
            if (ParserHelper.CheckPos(index, code, "false"))
            {
                index += 5;
                if (index >= code.Length) return (new _constFunction(new ObjectHolder(false, "bool")), true);
                else if (!char.IsLetter(code[index])) return (new _constFunction(new ObjectHolder(false, "bool")), true);
            }
            index = backupIndex;
            return (null, false);
        }
    }
}
