using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SylphScript;

namespace ConsoleSylph.Functions
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
                Console.Write((string)AssignedParameters[0].GetResult(variableHolder).Object);
                return ObjectHolder.Null;
            }
            if (AssignedParameters[0].AssignedReturnType == "int")
            {
                Console.Write(Convert.ToString((int)AssignedParameters[0].GetResult(variableHolder).Object));
                return ObjectHolder.Null;
            }
            if (AssignedParameters[0].AssignedReturnType == "double")
            {
                Console.Write(Convert.ToString((double)AssignedParameters[0].GetResult(variableHolder).Object).Replace(',', '.'));
                return ObjectHolder.Null;
            }
            throw new InvalidOperationException("Invalid parameters!");
        }
    }
}
