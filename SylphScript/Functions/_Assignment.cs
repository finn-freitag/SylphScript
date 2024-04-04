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

        public Modifiers Modifiers => Modifiers.None;

        string variableName = "";
        IFunction value = null;
        bool asReference = false;
        bool Readonly = false;

        public _Assignment(string variableName, IFunction value, bool asReference, bool Readonly)
        {
            this.variableName = variableName;
            this.value = value;
            this.asReference = asReference;
            this.Readonly = Readonly;
        }

        public IFunction GetNewInstance()
        {
            return new _Assignment(variableName, value, asReference, Readonly);
        }

        public ObjectHolder GetResult(VariableHolder variableHolder)
        {
            var varContent = value.GetResult(variableHolder);
            if (asReference)
            {
                variableHolder.AddVariable(variableName, varContent, Readonly);
                return varContent;
            }
            else
            {
                ObjectHolder clone = varContent.Clone();
                variableHolder.AddVariable(variableName, clone, Readonly);
                return clone;
            }
        }
    }
}
