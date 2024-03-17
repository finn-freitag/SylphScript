using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.Functions
{
    public class @return : IFunction
    {
        public IFunction NextFunction { get; set; }
        public ReferenceName AssignedReturnType { get; set; }
        public IFunction[] AssignedParameters { get; set; }

        public ReferenceName FullName => "return";

        public ArgResPermutation Parameters => ArgResPermutation.Build()
            .Add("null", "object")
            .Add("null");

        public IFunction GetNewInstance()
        {
            return new @return();
        }

        public ObjectHolder GetResult(VariableHolder variableHolder)
        {
            if(variableHolder.ReturnCallbackFunc != null)
            {
                if(AssignedParameters.Length == 0)
                {
                    variableHolder.ReturnCallbackFunc(ObjectHolder.Null);
                }
                else
                {
                    variableHolder.ReturnCallbackFunc(AssignedParameters[0].GetResult(variableHolder));
                }
            }
            return ObjectHolder.Null;
        }
    }
}
