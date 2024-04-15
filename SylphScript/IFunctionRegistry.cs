using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript
{
    public interface IFunctionRegistry
    {
        List<IFunction> FunctionList { get; }
    }
}
