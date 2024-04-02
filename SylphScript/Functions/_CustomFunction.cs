using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.Functions
{
    public class _CustomFunction : IFunction
    {
        public IFunction NextFunction { get; set; }
        public ReferenceName AssignedReturnType { get; set; }
        public IFunction[] AssignedParameters { get; set; }

        public ReferenceName FullName { get; private set; } = "_customfunction";

        public ArgResPermutation Parameters { get; private set; } = ArgResPermutation.Build().Add("null");

        public ReferenceName ReferenceObject { get; set; }

        private IFunction code = null;

        private string[] parameterNames;

        public _CustomFunction(ReferenceName functionName, IFunction code, ArgResPermutation parameters, string[] parameterNames)
        {
            FullName = functionName;
            this.code = code;
            Parameters = parameters;
            this.parameterNames = parameterNames;
        }

        public IFunction GetNewInstance()
        {
            return new _CustomFunction(FullName, code, Parameters, parameterNames);
        }

        public ObjectHolder GetResult(VariableHolder variableHolder)
        {
            VariableHolder subHolder = variableHolder.GetSubHolder(FullName);

            if (AssignedParameters.Length != parameterNames.Length)
                throw new InvalidOperationException("Parameter error!");

            for(int i = 0; i < AssignedParameters.Length; i++)
            {
                subHolder.AddVariable(parameterNames[i], AssignedParameters[i].GetResult(variableHolder));
            }

            ObjectHolder result = ObjectHolder.Null;
            subHolder.ReturnCallbackFunc = (ObjectHolder oh) =>
            {
                result = oh;
            };
            Executor.Execute(code, subHolder);
            return result;
        }
    }
}
