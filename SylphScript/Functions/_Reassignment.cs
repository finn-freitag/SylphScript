using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.Functions
{
    public class _Reassignment : IFunction
    {
        public IFunction NextFunction { get; set; }
        public ReferenceName AssignedReturnType { get; set; }
        public IFunction[] AssignedParameters { get; set; }

        public ReferenceName FullName => "_reassignment";

        public ArgResPermutation Parameters
        {
            get
            {
                return ArgResPermutation.Build().Add(value.AssignedReturnType);
            }
        }

        string variableName = "";
        IFunction value = null;

        public _Reassignment(string variableName, IFunction value)
        {
            this.variableName = variableName;
            this.value = value;
        }

        public IFunction GetNewInstance()
        {
            return new _Reassignment(variableName, value);
        }

        public ObjectHolder GetResult(VariableHolder variableHolder)
        {
            ObjectHolder res = value.GetResult(variableHolder);
            variableHolder.SetVariable(variableName, res);
            return res;
        }
    }
}
