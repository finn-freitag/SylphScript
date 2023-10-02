using SylphScript.Functions;
using SylphScript.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.AdditionalParsers
{
    public class DoWhileParser : IAdditionalParser
    {
        public bool isTypeParser => false;

        public (IFunction Function, bool Success) Parse(ref int index, string code, VariableHolder vHolder)
        {
            if (ParserHelper.CheckPos(index, code, "do"))
            {
                VariableHolder subHolder = vHolder.GetSubHolder("dowhile");
                index += 2;

                ParserHelper.SkipSpace(ref index, code);
                if (index >= code.Length || code[index] != '{') return (null, false);
                index++;
                IFunction fulfilled = Parser.ParseMultiple(ref index, code, subHolder);
                ParserHelper.SkipSpace(ref index, code);
                if (index >= code.Length || code[index] != '}') return (null, false);
                index++;

                ParserHelper.SkipSpace(ref index, code);
                if (!ParserHelper.CheckPos(index, code, "while")) return (null, false);
                index += 5;

                ParserHelper.SkipSpace(ref index, code);
                if (code[index] != '(') return (null, false);
                index++;
                ParserHelper.SkipSpace(ref index, code);
                IFunction condition = null;
                if (code[index] == ')') return (null, false);
                condition = Parser.Parse(ref index, code, subHolder);
                ParserHelper.SkipSpace(ref index, code);
                if (code[index] != ')') return (null, false);
                else index++;

                _DoWhile doWhileLoop = new _DoWhile();
                doWhileLoop.AssignedParameters = new IFunction[] { condition, fulfilled };
                return (doWhileLoop, true);
            }
            else return (null, false);
        }
    }
}
