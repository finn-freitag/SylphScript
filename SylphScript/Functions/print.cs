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
                return ArgResPermutation.Build()
                    .Add("null", "string")
                    .Add("null", "int")
                    .Add("null", "double");
            } }

        public IFunction GetNewInstance()
        {
            return new print();
        }

        public ObjectHolder GetResult(VariableHolder variableHolder)
        {
            if (AssignedParameters.Length != 1) throw new InvalidOperationException("Invalid parameters!");
            if (AssignedParameters[0].AssignedReturnType == "string")
            {
                Console.WriteLine((string)AssignedParameters[0].GetResult(variableHolder).Object);
                return ObjectHolder.Null;
            }
            if (AssignedParameters[0].AssignedReturnType == "int")
            {
                Console.WriteLine(Convert.ToString((int)AssignedParameters[0].GetResult(variableHolder).Object));
                return ObjectHolder.Null;
            }
            if (AssignedParameters[0].AssignedReturnType == "double")
            {
                Console.WriteLine(Convert.ToString((double)AssignedParameters[0].GetResult(variableHolder).Object));
                return ObjectHolder.Null;
            }
            throw new InvalidOperationException("Invalid parameters!");
        }
    }
}
