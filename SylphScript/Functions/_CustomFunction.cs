using SylphScript.Helper;
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

        public Modifiers Modifiers { get; set; } = Modifiers.None;

        private IFunction code = null;

        private string[] parameterNames;

        private bool isConstructor = false;

        public _CustomFunction(ReferenceName functionName, IFunction code, ArgResPermutation parameters, string[] parameterNames, Modifiers modifiers)
        {
            FullName = functionName;
            this.code = code;
            Parameters = parameters;
            this.parameterNames = parameterNames;
            this.Modifiers = modifiers;
            if (code is _DummyFunction && parameterNames.Length == 0 && code.NextFunction == null)
                isConstructor = true;
        }

        public IFunction GetNewInstance()
        {
            return new _CustomFunction(FullName, code, Parameters, parameterNames, Modifiers);
        }

        public ObjectHolder GetResult(VariableHolder variableHolder)
        {
            if (isConstructor)
                return TypeHelper.CreateInstance(TypeRegistry.FindType(Parameters.permutation[0].Result));

            VariableHolder subHolder = variableHolder.GetSubHolder(FullName);

            if (AssignedParameters.Length != parameterNames.Length)
                throw new SylphException("Parameter error in function \"" + FullName + "\"!");

            for(int i = 0; i < AssignedParameters.Length; i++)
            {
                subHolder.AddVariable(parameterNames[i], AssignedParameters[i].GetResult(subHolder));
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
