using SylphScript.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript
{
    public static class Executor
    {
        public static void Execute(IFunction function)
        {
            Execute(function, new VariableHolder());
        }

        public static void Execute(IFunction function, VariableHolder vHolder)
        {
            IFunction func = function;
            while (func != null)
            {
                func.GetResult(vHolder);
                if (func is @return) func = null;
                else func = func.NextFunction;
            }
        }
    }
}
