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
            IFunction func = function;
            VariableHolder holder = new VariableHolder();
            while (func != null)
            {
                func.GetResult(holder);
                func = func.NextFunction;
            }
        }
    }
}
