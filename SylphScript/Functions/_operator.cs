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

        public ReferenceName AssignedReturnType { get; set; }

        public IFunction[] AssignedParameters { get; set; }

        public ReferenceName FullName => "_operator";

        public ArgResPermutation Parameters
        {
            get
            {
                ArgResPermutation res = ArgResPermutation.Build();
                for(int i = 0; i < OperatorRegistry.Operators.Count; i++)
                {
                    res.Add(OperatorRegistry.Operators[i].Result, OperatorRegistry.Operators[i].Type1, OperatorRegistry.Operators[i].Type2);
                }
                return res;
            }
        }

        public IFunction GetNewInstance()
        {
            return new _operator();
        }

        public ObjectHolder GetResult(VariableHolder variableHolder)
        {
            if (AssignedParameters.Length != 2) throw new InvalidOperationException("Operator requires two arguments!");
            IOperator @operator = OperatorRegistry.GetOperator(AssignedParameters[0].FullName, AssignedParameters[1].FullName);
            if (@operator == null) throw new InvalidOperationException("Operator not found!");
            return new ObjectHolder(@operator.Process(AssignedParameters[0].GetResult(variableHolder), AssignedParameters[1].GetResult(variableHolder)), @operator.Result);
        }
    }
}
