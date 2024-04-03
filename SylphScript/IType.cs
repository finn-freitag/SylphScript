using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript
{
    public interface IType
    {
        string Name { get; }
        List<IFunction> SubFunctions { get; }
        List<(string name, ObjectHolder defaultValue)> Variables { get; }
        VariableHolder ConvertToVHolder(object typeEquivalent);
        object AppendPropsFromVHolder(object originalTypeEquivalent, VariableHolder vHolder);
    }
}
