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
        public ReferenceName AssignedReturnType { get { return value.AssignedReturnType; } set { } }
        public IFunction[] AssignedParameters { get; set; }
        public ReferenceName FullName => "_assignment";
        public ArgResPermutation Parameters { get
            {
                return ArgResPermutation.Build().Add(value.AssignedReturnType);
            }
        }

        public ReferenceName ReferenceObject { get; set; }

        string variableName = "";
        IFunction value = null;
        bool asReference = false;

        public _Assignment(string variableName, IFunction value, bool asReference)
        {
            this.variableName = variableName;
            this.value = value;
            this.asReference = asReference;
        }

        public IFunction GetNewInstance()
        {
            return new _Assignment(variableName, value, asReference);
        }

        public ObjectHolder GetResult(VariableHolder variableHolder)
        {
            var varContent = value.GetResult(variableHolder);
            if (asReference)
                variableHolder.AddVariable(variableName, varContent);
            else
                variableHolder.AddVariable(variableName, varContent.Clone());
            return varContent;
        }
    }
}
