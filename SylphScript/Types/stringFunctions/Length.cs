using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.Types.stringFunctions
{
    public class Length : IFunction
    {
        public IFunction NextFunction { get; set; }
        public ReferenceName AssignedReturnType { get; set; }
        public IFunction[] AssignedParameters { get; set; }
        public ReferenceName ReferenceObject { get; set; }

        public Modifiers Modifiers => Modifiers.None;

        public ReferenceName FullName => "Length";

        public ArgResPermutation Parameters => ArgResPermutation.Build().Add("int");

        public IFunction GetNewInstance()
        {
            return new Length();
        }

        public ObjectHolder GetResult(VariableHolder variableHolder)
        {
            int length = ((string)variableHolder.GetVariable(ReferenceObject).Object).Length;
            return new ObjectHolder(length, "int");
        }
    }
}
