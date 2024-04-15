using SylphScript.Functions;
using SylphScript.Helper;
using SylphScript.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.AdditionalParsers
{
    public class ClassParser : IAdditionalParser
    {
        public bool isTypeParser => false;

        public (IFunction Function, bool Success) Parse(ref int index, string code, VariableHolder vHolder)
        {
            if (!ParserHelper.CheckPos(index, code, "class"))
                return (null, false);
            index += 5;
            ParserHelper.SkipSpace(ref index, code);
            string classname = ParserHelper.GetIdentifier(ref index, code);
            ParserHelper.SkipSpace(ref index, code);
            if (code[index] != '{')
                return (null, false);
            index++;
            VariableHolder classVHolder = new VariableHolder();
            classVHolder.CurrentFunctionRegistry = new SubFunctionsRegistry();
            CustomType ctype = new CustomType()
            {
                Name = classname,
                SubFunctions = classVHolder.CurrentFunctionRegistry,
                Variables = classVHolder.Variables.Select((pair) => { return (pair.Key, pair.Value); }).ToList(),
                ReadonlyVars = classVHolder.ReadonlyVariables
            };
            TypeRegistry.Types.Add(ctype);
            Parser.ParseMultiple(ref index, code, classVHolder, () =>
            {
                for (int i = 0; i < classVHolder.CurrentFunctionRegistry.FunctionList.Count; i++)
                {
                    if (classVHolder.CurrentFunctionRegistry.FunctionList[i].FullName == classname)
                    {
                        FunctionsRegistry.Functions.Add(classVHolder.CurrentFunctionRegistry.FunctionList[i]);
                        classVHolder.CurrentFunctionRegistry.FunctionList.RemoveAt(i);
                        i--;
                    }
                }
                ctype.SubFunctions = classVHolder.CurrentFunctionRegistry;
                ctype.Variables = classVHolder.Variables.Select((pair) => { return (pair.Key, pair.Value); }).ToList();
            });
            if (index >= code.Length || code[index] != '}')
                return (null, false);
            index++;
            return (new _DummyFunction(), true);
        }
    }
}
