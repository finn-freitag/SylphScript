using SylphScript.Functions;
using SylphScript.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.AdditionalParsers
{
    public class ReturnParser : IAdditionalParser
    {
        public bool isTypeParser => false;

        public (IFunction Function, bool Success) Parse(ref int index, string code, VariableHolder vHolder)
        {
            if (ParserHelper.CheckPos(index, code, "return"))
            {
                index += 6;
                ParserHelper.SkipSpace(ref index, code);
                IFunction returnValue;
                returnValue = Parser.Parse(ref index, code, vHolder);
                _return Return = new _return();
                Return.AssignedParameters = new IFunction[] { returnValue };
                return (Return, true);
            }
            else return (null, false);
        }
    }
}
