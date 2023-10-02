using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SylphScript;

namespace ConsoleSylph.Functions
{
    public class readKey : IFunction
    {
        public IFunction NextFunction { get; set; }
        public ReferenceName AssignedReturnType { get; set; }
        public IFunction[] AssignedParameters { get; set; }

        public ReferenceName FullName => "readKey";

        public ArgResPermutation Parameters => ArgResPermutation.Build().Add("null");

        public IFunction GetNewInstance()
        {
            return new readKey();
        }

        public ObjectHolder GetResult(VariableHolder variableHolder)
        {
            Console.ReadKey();
            return ObjectHolder.Null;
        }
    }
}
