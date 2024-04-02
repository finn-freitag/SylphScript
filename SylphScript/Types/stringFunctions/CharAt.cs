using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.Types.stringFunctions
{
    public class CharAt : IFunction
    {
        public IFunction NextFunction { get; set; }
        public ReferenceName AssignedReturnType { get; set; }
        public IFunction[] AssignedParameters { get; set; }

        public ReferenceName FullName => "CharAt";

        public ArgResPermutation Parameters => ArgResPermutation.Build().Add("char", "int");

        public ReferenceName ReferenceObject { get; set; }

        public IFunction GetNewInstance()
        {
            return new CharAt();
        }

        public ObjectHolder GetResult(VariableHolder variableHolder)
        {
            char c = ((string)variableHolder.GetVariable(ReferenceObject).Object)[(int)AssignedParameters[0].GetResult(variableHolder).Object];
            return new ObjectHolder(c, "char");
        }
    }
}
