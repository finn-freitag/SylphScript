using SylphScript.Functions;
using SylphScript.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.AdditionalParsers
{
    public class WhileParser : IAdditionalParser
    {
        public bool isTypeParser => false;

        public (IFunction Function, bool Success) Parse(ref int index, string code, VariableHolder vHolder)
        {
            if (ParserHelper.CheckPos(index, code, "while"))
            {
                VariableHolder subHolder = vHolder.GetSubHolder("while");
                index += 5;
                ParserHelper.SkipSpace(ref index, code);
                if (code[index] != '(') return (null, false);
                index++;

                ParserHelper.SkipSpace(ref index, code);
                IFunction condition = null;
                if (code[index] != ')')
                {
                    condition = Parser.Parse(ref index, code, subHolder);
                    ParserHelper.SkipSpace(ref index, code);
                    if (code[index] != ')') return (null, false);
                    else index++;
                }
                else index++;

                ParserHelper.SkipSpace(ref index, code);
                if (index >= code.Length || code[index] != '{')
                {
                    if (condition == null) return (null, false);
                    _While whileL = new _While();
                    whileL.AssignedParameters = new IFunction[] { condition, null };
                    return (whileL, true);
                }
                index++;
                IFunction fulfilled = Parser.ParseMultiple(ref index, code, subHolder);
                ParserHelper.SkipSpace(ref index, code);
                if (index >= code.Length || code[index] != '}') return (null, false);
                index++;

                _While whileLoop = new _While();
                whileLoop.AssignedParameters = new IFunction[] { condition, fulfilled };
                return (whileLoop, true);
            }
            else return (null, false);
        }
    }
}
