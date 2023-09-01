using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.Functions
{
    public class _ForLoop3Params : IFunction
    {
        public IFunction NextFunction { get; set; }
        public ReferenceName AssignedReturnType { get; set; }
        public IFunction[] AssignedParameters { get; set; }

        public ReferenceName FullName => "_forloop3params";

        public ArgResPermutation Parameters => ArgResPermutation.Build()
            .Add("null", "object", "bool", "object", "object")
            .Add("null", "null", "bool", "object", "object")
            .Add("null", "object", "null", "object", "object")
            .Add("null", "object", "bool", "null", "object")
            .Add("null", "object", "null", "null", "object")
            .Add("null", "null", "null", "object", "object")
            .Add("null", "null", "bool", "null", "object")
            .Add("null", "null", "null", "null", "object");

        public IFunction GetNewInstance()
        {
            return new _ForLoop3Params();
        }

        public ObjectHolder GetResult(VariableHolder variableHolder)
        {
            VariableHolder subHolder = variableHolder.GetSubHolder("for");
            if (AssignedParameters[0] != null) AssignedParameters[0].GetResult(subHolder);
            while(getConditionResult(subHolder))
            {
                AssignedParameters[3].GetResult(subHolder);
                if (AssignedParameters[2] != null) AssignedParameters[2].GetResult(subHolder);
            }
            return ObjectHolder.Null;
        }

        private bool getConditionResult(VariableHolder subHolder)
        {
            if (AssignedParameters[1] == null) return true;
            else return (bool)AssignedParameters[1].GetResult(subHolder).Object;
        }
    }
}
