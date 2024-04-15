using SylphScript.Types.stringFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.Types
{
    public class @string : IType
    {
        public string Name => "string";

        public IFunctionRegistry SubFunctions => new SubFunctionsRegistry()
        {
            FunctionList = new List<IFunction>()
            {
                new CharAt(),
                new Length(),
            }
        };

        public List<(string name, ObjectHolder defaultValue)> Variables => new List<(string name, ObjectHolder defaultValue)>();

        public object AppendPropsFromVHolder(object originalTypeEquivalent, VariableHolder vHolder)
        {
            return originalTypeEquivalent;
        }

        public VariableHolder ConvertToVHolder(object typeEquivalent)
        {
            return null;
        }

        public object Clone(object typeEquivalent)
        {
            return typeEquivalent;
        }
    }
}
