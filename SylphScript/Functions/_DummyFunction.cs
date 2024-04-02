using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.Functions
{
    public class _DummyFunction : IFunction
    {
        public IFunction NextFunction { get; set; }
        public ReferenceName AssignedReturnType { get; set; }
        public IFunction[] AssignedParameters { get; set; }

        public ReferenceName FullName => "_dummy";

        public ArgResPermutation Parameters => ArgResPermutation.Build()
            .Add("null");

        public ReferenceName ReferenceObject { get; set; }

        public IFunction GetNewInstance()
        {
            return new _DummyFunction();
        }

        public ObjectHolder GetResult(VariableHolder variableHolder)
        {
            return ObjectHolder.Null;
        }
    }
}
