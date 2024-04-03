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

        public ReferenceName ReferenceObject { get; set; }

        string variableName = "";
        IFunction value = null;
        bool asReference = false;
        bool keepRefs = false;

        public _Reassignment(string variableName, IFunction value, bool asReference, bool keepRefs)
        {
            this.variableName = variableName;
            this.value = value;
            this.asReference = asReference;
            this.keepRefs = keepRefs;
        }

        public IFunction GetNewInstance()
        {
            return new _Reassignment(variableName, value, asReference, keepRefs);
        }

        public ObjectHolder GetResult(VariableHolder variableHolder)
        {
            var varContent = value.GetResult(variableHolder);
            if (asReference)
                variableHolder.SetVariable(variableName, varContent, keepRefs);
            else
                variableHolder.SetVariable(variableName, varContent.Clone(), keepRefs);
            return varContent;
        }
    }
}
