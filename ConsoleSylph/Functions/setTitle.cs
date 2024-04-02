using SylphScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSylph.Functions
{
    public class setTitle : IFunction
    {
        public IFunction NextFunction { get; set; }
        public ReferenceName AssignedReturnType { get; set; }
        public IFunction[] AssignedParameters { get; set; }

        public ReferenceName FullName => "setTitle";

        public ArgResPermutation Parameters => ArgResPermutation.Build()
            .Add("null", "string");

        public ReferenceName ReferenceObject { get; set; }

        public IFunction GetNewInstance()
        {
            return new setTitle();
        }

        public ObjectHolder GetResult(VariableHolder variableHolder)
        {
            Console.Title = (string)AssignedParameters[0].GetResult(variableHolder).Object;
            return ObjectHolder.Null;
        }
    }
}
