using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.Functions
{
    public class _While : IFunction
    {
        public IFunction NextFunction { get; set; }
        public ReferenceName AssignedReturnType { get; set; }
        public IFunction[] AssignedParameters { get; set; }

        public ReferenceName FullName => "_while";

        public ArgResPermutation Parameters => ArgResPermutation.Build()
            .Add("null", "bool", "object")
            .Add("null", "null", "object")
            .Add("null", "bool", "null");

        public ReferenceName ReferenceObject { get; set; }

        public Modifiers Modifiers => Modifiers.None;

        public IFunction GetNewInstance()
        {
            return new _While();
        }

        public ObjectHolder GetResult(VariableHolder variableHolder)
        {
            VariableHolder subHolder = variableHolder.GetSubHolder("while");
            while (getConditionResult(subHolder))
            {
                if (AssignedParameters[1] != null) Executor.Execute(AssignedParameters[1], subHolder);
            }
            return ObjectHolder.Null;
        }

        private bool getConditionResult(VariableHolder subHolder)
        {
            if (AssignedParameters[0] == null) return true;
            else return (bool)AssignedParameters[0].GetResult(subHolder).Object;
        }
    }
}
