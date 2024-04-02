using SylphScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSylph.Functions
{
    public class beep : IFunction
    {
        public IFunction NextFunction { get; set; }
        public ReferenceName AssignedReturnType { get; set; }
        public IFunction[] AssignedParameters { get; set; }

        public ReferenceName FullName => "beep";

        public ArgResPermutation Parameters => ArgResPermutation.Build()
            .Add("null")
            .Add("null", "int", "int");

        public ReferenceName ReferenceObject { get; set; }

        public IFunction GetNewInstance()
        {
            return new beep();
        }

        public ObjectHolder GetResult(VariableHolder variableHolder)
        {
            if(AssignedParameters.Length == 2)
            {
                Console.Beep((int)AssignedParameters[0].GetResult(variableHolder).Object, (int)AssignedParameters[1].GetResult(variableHolder).Object);
            }
            else
            {
                Console.Beep();
            }
            return ObjectHolder.Null;
        }
    }
}
