using SylphScript.Functions;
using SylphScript.TypeParsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript
{
    public static class FunctionsRegistry
    {
        public static List<IFunction> Functions = new List<IFunction>()
        {
            new print(),
            new readLine(),
        };
    }
}
