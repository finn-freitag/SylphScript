using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.Types
{
    public class @double : IType
    {
        public string Name => "double";

        public IFunctionRegistry SubFunctions => new SubFunctionsRegistry();

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
