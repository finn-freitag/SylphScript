using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.Functions
{
    public class _getVariable : IFunction
    {
        public IFunction NextFunction { get; set; }
        public ReferenceName AssignedReturnType { get { return type; } set { } }
        public IFunction[] AssignedParameters { get; set; }

        public ReferenceName FullName => "_getVariable";

        public ArgResPermutation Parameters
        {
            get
            {
                return ArgResPermutation.Build().Add(type);
            }
        }

        private string VariableName = "";
        private ReferenceName type = "";

        public _getVariable(string VariableName, ReferenceName type)
        {
            this.VariableName = VariableName;
            this.type = type;
        }

        public IFunction GetNewInstance()
        {
            return new _getVariable(VariableName, type);
        }

        public ObjectHolder GetResult(VariableHolder variableHolder)
        {
            return variableHolder.GetVariable(VariableName);
        }
    }
}
