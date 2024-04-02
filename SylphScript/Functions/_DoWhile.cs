using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.Functions
{
    public class _DoWhile : IFunction
    {
        public IFunction NextFunction { get; set; }
        public ReferenceName AssignedReturnType { get; set; }
        public IFunction[] AssignedParameters { get; set; }

        public ReferenceName FullName => "_dowhile";

        public ArgResPermutation Parameters => ArgResPermutation.Build()
            .Add("null", "bool", "object");

        public ReferenceName ReferenceObject { get; set; }

        public IFunction GetNewInstance()
        {
            return new _DoWhile();
        }

        public ObjectHolder GetResult(VariableHolder variableHolder)
        {
            VariableHolder subHolder = variableHolder.GetSubHolder("dowhile");
            do
            {
                Executor.Execute(AssignedParameters[1], subHolder);
            } while (getConditionResult(subHolder));
            return ObjectHolder.Null;
        }

        private bool getConditionResult(VariableHolder subHolder)
        {
            return (bool)AssignedParameters[0].GetResult(subHolder).Object;
        }
    }
}
