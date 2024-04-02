using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.Functions
{
    public class _operator : IFunction
    {
        public IFunction NextFunction { get; set; }

        public ReferenceName AssignedReturnType { get { return op.Result; } set { } }

        public IFunction[] AssignedParameters { get; set; }

        public ReferenceName FullName => "_operator";

        public ArgResPermutation Parameters
        {
            get
            {
                if (op.IsOneParameterOperator) return ArgResPermutation.Build().Add(op.Result, op.Type1);
                else return ArgResPermutation.Build().Add(op.Result, op.Type1, op.Type2);
            }
        }

        public ReferenceName ReferenceObject { get; set; }

        private IOperator op = null;

        public _operator(IOperator op)
        {
            this.op = op;
        }

        public IFunction GetNewInstance()
        {
            return new _operator(op);
        }

        public ObjectHolder GetResult(VariableHolder variableHolder)
        {
            if (op.IsOneParameterOperator)
            {
                return op.Process(AssignedParameters[0], null, variableHolder);
            }
            else
            {
                return op.Process(AssignedParameters[0], AssignedParameters[1], variableHolder);
            }
        }
    }
}
