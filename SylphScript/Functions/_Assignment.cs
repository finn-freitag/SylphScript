using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.Functions
{
    public class _Assignment : IFunction
    {
        public IFunction NextFunction { get; set; }
        public ReferenceName AssignedReturnType { get; set; }
        public IFunction[] AssignedParameters { get; set; }
        public ReferenceName FullName => "_assignment";
        public ArgResPermutation Parameters { get
            {
                return ArgResPermutation.Build().Add(value.AssignedReturnType);
            } }

        string variableName = "";
        IFunction value = null;

        public _Assignment(string variableName, IFunction value)
        {
            this.variableName = variableName;
            this.value = value;
        }

        public IFunction GetNewInstance()
        {
            return new _Assignment(variableName, value);
        }

        public ObjectHolder GetResult(VariableHolder variableHolder)
        {
            var varContent = value.GetResult(variableHolder);
            variableHolder.AddVariable(variableName, value.GetResult(variableHolder));
            return varContent;
        }
    }
}
