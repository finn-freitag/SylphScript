using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.Types
{
    public class CustomType : IType
    {
        public string Name { get; set; } = "";

        public IFunctionRegistry SubFunctions { get; set; } = new SubFunctionsRegistry();

        public List<(string name, ObjectHolder defaultValue)> Variables { get; set; } = new List<(string name, ObjectHolder defaultValue)>();

        public List<string> ReadonlyVars = new List<string>();

        public object AppendPropsFromVHolder(object originalTypeEquivalent, VariableHolder vHolder)
        {
            return originalTypeEquivalent;
        }

        public object Clone(object typeEquivalent)
        {
            return new object();
        }

        public VariableHolder ConvertToVHolder(object typeEquivalent)
        {
            return new VariableHolder();
        }
    }
}
