using SylphScript.Functions;
using SylphScript.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.AdditionalParsers
{
    public class ForLoop3ParamsParser : IAdditionalParser
    {
        public bool isTypeParser => false;

        public (IFunction Function, bool Success) Parse(ref int index, string code, VariableHolder vHolder)
        {
            if (ParserHelper.CheckPos(index, code, "for"))
            {
                VariableHolder subHolder = vHolder.GetSubHolder("for");
                index += 3;
                ParserHelper.SkipSpace(ref index, code);
                if (code[index] != '(') return (null, false);
                index++;

                ParserHelper.SkipSpace(ref index, code);
                IFunction start = null;
                if (code[index] != ',' && code[index] != ';')
                {
                    start = Parser.Parse(ref index, code, subHolder);
                    ParserHelper.SkipSpace(ref index, code);
                    if (code[index] != ',' && code[index] != ';') return (null, false);
                    else index++;
                }
                else index++;

                ParserHelper.SkipSpace(ref index, code);
                IFunction condition = null;
                if (code[index] != ',' && code[index] != ';')
                {
                    condition = Parser.Parse(ref index, code, subHolder);
                    ParserHelper.SkipSpace(ref index, code);
                    if (code[index] != ',' && code[index] != ';') return (null, false);
                    else index++;
                }
                else index++;

                ParserHelper.SkipSpace(ref index, code);
                IFunction repeating = null;
                if (code[index] != ')')
                {
                    repeating = Parser.Parse(ref index, code, subHolder);
                    ParserHelper.SkipSpace(ref index, code);
                    if (code[index] != ')') return (null, false);
                    else index++;
                }
                else index++;

                ParserHelper.SkipSpace(ref index, code);
                if (index >= code.Length || code[index] != '{') return (null, false);
                index++;
                IFunction fulfilled = Parser.Parse(ref index, code, subHolder);
                ParserHelper.SkipSpace(ref index, code);
                if (index >= code.Length || code[index] != '}') return (null, false);
                index++;

                _ForLoop3Params forLoop = new _ForLoop3Params();
                forLoop.AssignedParameters = new IFunction[] { start, condition, repeating, fulfilled };
                return (forLoop, true);
            }
            else return (null, false);
        }
    }
}
