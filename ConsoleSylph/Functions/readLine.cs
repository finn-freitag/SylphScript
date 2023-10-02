using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SylphScript;

namespace ConsoleSylph.Functions
{
    public class readLine : IFunction
    {
        public IFunction NextFunction { get; set; }
        public ReferenceName AssignedReturnType { get; set; }
        public IFunction[] AssignedParameters { get; set; }
        public ReferenceName FullName => "readLine";
        public ArgResPermutation Parameters { get
            {
                return ArgResPermutation.Build().Add("string");
            } }

        public IFunction GetNewInstance()
        {
            return new readLine();
        }

        public ObjectHolder GetResult(VariableHolder variableHolder)
        {
            string str = Console.ReadLine();
            return new ObjectHolder(str, "string");
        }
    }
}
