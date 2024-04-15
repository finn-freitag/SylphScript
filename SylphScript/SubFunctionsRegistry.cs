using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript
{
    public class SubFunctionsRegistry : IFunctionRegistry
    {
        public List<IFunction> FunctionList { get; set; } = new List<IFunction>();
    }
}
