using SylphScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSylph.Functions
{
    public class sylph : IFunction
    {
        public IFunction NextFunction { get; set; }
        public ReferenceName AssignedReturnType { get; set; }
        public IFunction[] AssignedParameters { get; set; }
        public ReferenceName ReferenceObject { get; set; }

        public ReferenceName FullName => "sylph";

        public ArgResPermutation Parameters => ArgResPermutation.Build().Add("null","string");

        public Modifiers Modifiers => Modifiers.None;

        public IFunction GetNewInstance()
        {
            return new sylph();
        }

        public ObjectHolder GetResult(VariableHolder variableHolder)
        {
            Executor.Execute(Parser.Parse((string)AssignedParameters[0].GetResult(variableHolder).Object, variableHolder), variableHolder);
            return ObjectHolder.Null;
        }
    }
}
