using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.Functions
{
    public class _If : IFunction
    {
        public IFunction NextFunction { get; set; }
        public ReferenceName AssignedReturnType { get { return "null"; } set { } }
        public IFunction[] AssignedParameters { get; set; }

        public ReferenceName FullName => "_if";

        public ArgResPermutation Parameters => ArgResPermutation.Build()
            .Add("null", "bool", "object")
            .Add("null", "bool", "object", "object");

        public ReferenceName ReferenceObject { get; set; }

        public Modifiers Modifiers => Modifiers.None;

        public _If()
        {

        }

        public IFunction GetNewInstance()
        {
            return new _If();
        }

        public ObjectHolder GetResult(VariableHolder variableHolder)
        {
            bool run = (bool)AssignedParameters[0].GetResult(variableHolder).Object;
            if (run) Executor.Execute(AssignedParameters[1], variableHolder.GetSubHolder("if"));
            else if (AssignedParameters.Length == 3) Executor.Execute(AssignedParameters[2], variableHolder.GetSubHolder("else"));
            return ObjectHolder.Null;
        }
    }
}
