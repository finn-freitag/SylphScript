using SylphScript.Functions;
using SylphScript.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.AdditionalParsers
{
    public class IfParser : IAdditionalParser
    {
        public bool isTypeParser => false;

        public (IFunction Function, bool Success) Parse(ref int index, string code, VariableHolder vHolder)
        {
            if (ParserHelper.CheckPos(index, code, "if"))
            {
                index += 2;
                ParserHelper.SkipSpace(ref index, code);
                if (code[index] != '(') return (null, false);
                index++;
                IFunction condition = Parser.Parse(ref index, code, vHolder);
                if(condition.AssignedReturnType != "bool")
                {
                    IConversion conversion = ConversionRegistry.GetImplicitConversion(condition.AssignedReturnType, "bool");
                    if (conversion == null) return (null, false);
                    _implConvertFunction con = new _implConvertFunction(conversion);
                    con.AssignedParameters = new IFunction[] { condition };
                    condition = con;
                }
                ParserHelper.SkipSpace(ref index, code);
                if (code[index] != ')') return (null, false);
                index++;
                ParserHelper.SkipSpace(ref index, code);
                if (code[index] != '{') return (null, false);
                index++;
                IFunction fulfilled = Parser.ParseMultiple(ref index, code, vHolder.GetSubHolder("if"));
                ParserHelper.SkipSpace(ref index, code);
                if (index >= code.Length || code[index] != '}') return (null, false);
                index++;
                ParserHelper.SkipSpace(ref index, code);
                IFunction Else = null;
                if (index < code.Length && ParserHelper.CheckPos(index, code, "else"))
                {
                    index += 4;
                    ParserHelper.SkipSpace(ref index, code);
                    if (code[index] != '{') return (null, false);
                    index++;
                    Else = Parser.ParseMultiple(ref index, code, vHolder.GetSubHolder("else"));
                    ParserHelper.SkipSpace(ref index, code);
                    if (index >= code.Length || code[index] != '}') return (null, false);
                    index++;
                }
                if(Else == null)
                {
                    _If If = new _If();
                    If.AssignedParameters = new IFunction[] { condition, fulfilled };
                    return (If, true);
                }
                else
                {
                    _If If = new _If();
                    If.AssignedParameters = new IFunction[] { condition, fulfilled, Else };
                    return (If, true);
                }
            }
            else return (null, false);
        }
    }
}
