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
                return ArgResPermutation.Build().Add("object", "string", "object");
            } }

        public IFunction GetNewInstance()
        {
            return new _Assignment();
        }

        public ObjectHolder GetResult(VariableHolder variableHolder)
        {
            if (AssignedParameters.Length != 2 || AssignedParameters[0].FullName != "string") throw new InvalidOperationException("Invalid parameters!");
            var varContent = AssignedParameters[1].GetResult(variableHolder);
            variableHolder.AddVariable((string)AssignedParameters[0].GetResult(variableHolder).Object, varContent);
            return varContent;
        }
    }
}
