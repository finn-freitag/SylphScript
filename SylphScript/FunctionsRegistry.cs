using SylphScript.Functions;
using SylphScript.AdditionalParsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript
{
    public class FunctionsRegistry : IFunctionRegistry
    {
        public static FunctionsRegistry Global = new FunctionsRegistry();

        public static List<IFunction> Functions = new List<IFunction>()
        {
            
        };

        public List<IFunction> FunctionList => Functions;
    }
}
