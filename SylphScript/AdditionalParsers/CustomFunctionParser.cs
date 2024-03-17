using SylphScript.Functions;
using SylphScript.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.AdditionalParsers
{
    public class CustomFunctionParser : IAdditionalParser
    {
        public bool isTypeParser => false;

        public (IFunction Function, bool Success) Parse(ref int index, string code, VariableHolder vHolder)
        {
            string firstID = ParserHelper.GetIdentifier(ref index, code);
            if (firstID == "") return (null, false);
            ParserHelper.SkipSpace(ref index, code);
            string secondID = ParserHelper.GetIdentifier(ref index, code);
            ParserHelper.SkipSpace(ref index, code);
            if (code[index] != '(') return (null, false);
            index++;
            ParserHelper.SkipSpace(ref index, code);
            List<(string Type, string Name)> parameter = new List<(string, string)>();
            while (code[index] != ')')
            {
                string paramType = ParserHelper.GetIdentifier(ref index, code);
                if (paramType == "") return (null, false);
                ParserHelper.SkipSpace(ref index, code);
                string paramName = ParserHelper.GetIdentifier(ref index, code);
                ParserHelper.SkipSpace(ref index, code);
                if (code[index] != ',' && code[index] != ')') return (null, false);
                if (code[index] == ',') index++;
                ParserHelper.SkipSpace(ref index, code);
                parameter.Add((paramType, paramName));

                if (paramType.Length == 0 || paramName.Length == 0) return (null, false);
                vHolder.AddVariable(paramName, new ObjectHolder(null, paramType));
            }
            index++;
            ParserHelper.SkipSpace(ref index, code);
            if (index >= code.Length) return (null, false);
            if (code[index] != '{') return (null, false);
            index++;
            if (index >= code.Length) return (null, false);
            IFunction funcBody = Parser.ParseMultiple(ref index, code, vHolder.GetSubHolder(secondID));
            ParserHelper.SkipSpace(ref index, code);
            if (index >= code.Length || code[index] != '}') return (null, false);
            index++;

            _CustomFunction customFunction = new _CustomFunction(secondID, funcBody,
                ArgResPermutation.Build().Add(firstID, parameter.Select((val) => { return (ReferenceName)val.Type; }).ToArray()),
                parameter.Select((val) => { return val.Name; }).ToArray()
            );

            FunctionsRegistry.Functions.Add(customFunction);

            return (new _DummyFunction(), true);
        }
    }
}
