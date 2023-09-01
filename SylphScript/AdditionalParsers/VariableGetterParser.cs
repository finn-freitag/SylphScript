using SylphScript.Functions;
using SylphScript.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.AdditionalParsers
{
    public class VariableGetterParser : IAdditionalParser
    {
        public bool isTypeParser => true;

        public (IFunction Function, bool Success) Parse(ref int index, string code, VariableHolder vHolder)
        {
            string identifier = ParserHelper.GetIdentifier(ref index, code);
            if(identifier != "" && code[index]!='(')
            {
                if (!vHolder.VariableExist(identifier)) return (null, false);
                return (new _getVariable(identifier, vHolder.GetVariable(identifier).TypeFullName), true);
            }
            else
            {
                return (null, false);
            }
        }
    }
}
