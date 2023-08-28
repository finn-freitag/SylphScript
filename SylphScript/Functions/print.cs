using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.Functions
{
    public class print : IFunction
    {
        public IFunction NextFunction { get; set; }
        public ReferenceName AssignedReturnType { get; set; }
        public IFunction[] AssignedParameters { get; set; }
        public ReferenceName FullName => "print";
        public ArgResPermutation Parameters { get
            {
                return ArgResPermutation.Build().Add("null", "string");
            } }

        public IFunction GetNewInstance()
        {
            return new print();
        }

        public ObjectHolder GetResult(VariableHolder variableHolder)
        {
            if (AssignedParameters.Length != 1 || (AssignedParameters[0].FullName != "string")) throw new InvalidOperationException("Invalid parameters!");
            Console.WriteLine((string)AssignedParameters[0].GetResult(variableHolder).Object);
            return ObjectHolder.Null;
        }
    }
}
